using Newtonsoft.Json;
using SocketIOClient;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Rpc;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using System.Globalization;

namespace Plutonication
{
    public class PlutonicationWalletClient
    {
        private static SocketIO? client;

        private static string roomKey = "";

        /// <summary>
        /// Handles communication with the Plutonication server, sending and receiving payloads and raw signatures.
        /// </summary>
        /// <param name="ac">The credentials required for connecting to the Plutonication server.</param>
        /// <param name="pubkey">The public key associated with the wallet.</param>
        /// <param name="signPayload">Callback function to handle payload signing.</param>
        /// <param name="signRaw">Callback function to handle raw message signing.</param>
        /// <param name="update">Callback function to handle extrinsic updates</param>
        /// <param name="onConnected">Callback function to handle the successful Connection to the Plutonication Server.</param>
        /// <param name="onDisconnected">Callback function to handle the disconnection from the Plutonication Server.</param>
        /// <param name="onReconnectAttempt">Callback function to handle the reconnection attempt to the Plutonication Server.</param>
        /// <param name="onReconnected">Callback function to handle the reconnection to the Plutonication Server.</param>
        /// <param name="onReconnectFailed">Callback function to handle the reconnect failed to the Plutonication Server.</param>
        /// <param name="onConfirmDAppConnection">Callback function to handle the confirmation of the dApp connection to the Plutonication Server.</param>
        /// <param name="onDAppDisconnected">Callback function to handle the disconnection of the respective dApp.</param>
        /// <returns></returns>
        /// <exception cref="PlutonicationConnectionException">Error when unable to establish connection with the websocket server provided in the access credentials.</exception>
        public static async Task InitializeAsync(
            AccessCredentials ac,
            string pubkey,
            Func<UnCheckedExtrinsic, RuntimeVersion, Task> signPayload,
            Func<RawMessage, Task> signRaw,
            Action<Update>? update = null,
            EventHandler? onConnected = null,
            EventHandler<string>? onDisconnected = null,
            EventHandler<int>? onReconnectAttempt = null,
            EventHandler<int>? onReconnected = null,
            EventHandler? onReconnectFailed = null,
            Action? onConfirmDAppConnection = null,
            Action? onDAppDisconnected = null
        )
        {
            roomKey = ac.Key;
            client = new SocketIO(ac.Url);

            client.On("sign_payload", payloadJson =>
            {
                Plutonication.Payload[]? payloads = JsonConvert.DeserializeObject<Plutonication.Payload[]>(payloadJson.ToString());

                if (payloads is null || !payloads.Any())
                {
                    throw new WrongMessageReceivedException();
                }

                Plutonication.Payload payload = payloads[0];

                if (payload.tip is null || payload.specVersion is null ||
                    payload.transactionVersion is null || payload.nonce is null)
                {
                    throw new WrongMessageReceivedException();
                }                

                byte[] methodBytes = Utils.HexToByteArray(payload.method);

                List<byte> methodParameters = new List<byte>();

                for (int i = 2; i < methodBytes.Length; i++)
                {
                    methodParameters.Add(methodBytes[i]);
                }

                Method method = new Method(methodBytes[0], methodBytes[1], methodParameters.ToArray());

                Hash eraHash = new Hash();
                eraHash.Create(Utils.HexToByteArray(payload.era));

                Hash blockHash = new Hash();
                blockHash.Create(payload.blockHash);

                Hash genesisHash = new Hash();
                genesisHash.Create(Utils.HexToByteArray(payload.genesisHash));

                RuntimeVersion runtime = new RuntimeVersion
                {
                    ImplVersion = payload.version,
                    SpecVersion = HexStringToUint(payload.specVersion),
                    TransactionVersion = HexStringToUint(payload.transactionVersion),
                };

                ChargeType charge;

                if (payload.tip.Length == 34)
                {
                    charge = new ChargeTransactionPayment(HexStringToUint(payload.tip));
                }
                else
                {
                    int _p = 0;

                    charge = new ChargeAssetTxPayment(0, 0);
                    charge.Decode(Utils.HexToByteArray(payload.tip), ref _p);
                }

                Account account = new Account();
                account.Create(KeyType.Sr25519, Utils.GetPublicKeyFrom(payload.address));

                UnCheckedExtrinsic unCheckedExtrinsic = new UnCheckedExtrinsic(true, account, method, Era.Decode(Utils.HexToByteArray(payload.era)),
                    HexStringToUint(payload.nonce), charge, genesisHash, blockHash);

                Task _signPayloadTask = signPayload.Invoke(unCheckedExtrinsic, runtime);
            });

            client.On("sign_raw", rawJson =>
            {
                RawMessage[]? rawMessages = JsonConvert.DeserializeObject<RawMessage[]>(rawJson.ToString());

                if (rawMessages is null || !rawMessages.Any())
                {
                    // Wrong message received
                    return;
                }

                Task signRawTask = signRaw.Invoke(rawMessages[0]);
            });

            client.On("update", receivedUpdate =>
            {
                Update[]? receivedUpdates = JsonConvert.DeserializeObject<Update[]>(receivedUpdate.ToString());

                if (receivedUpdates is null || !receivedUpdates.Any())
                {
                    // Wrong message received
                    return;
                }

                update?.Invoke(receivedUpdates[0]);
            });

            // Handle the scenario where dApp connects after the Wallet.
            client.On("dapp_connected", _ =>
            {
                Task sendPublicKeyTask = SendPublicKeyAsync(pubkey);
            });

            // Confirm dApp connection
            client.On("confirm_dapp_connection", _ =>
            {
                onConfirmDAppConnection?.Invoke();
            });

            // Handle the dApp disconnection
            client.On("disconnect", _ =>
            {
                onDAppDisconnected?.Invoke();
            });

            // Handle client connection
            client.OnConnected += onConnected;
            client.OnDisconnected += onDisconnected;
            client.OnReconnectAttempt += onReconnectAttempt;
            client.OnReconnected += onReconnected;
            client.OnReconnected += (object sender, int _) =>
            {
                Task sendPublicKeyTask = SendPublicKeyAsync(pubkey);
            };
            client.OnReconnectFailed += onReconnectFailed;

            // Connect to WebSocket
            try
            {
                await client.ConnectAsync();
            }
            catch (SocketIOClient.ConnectionException)
            {
                throw new PlutonicationConnectionException();
            }

            await SendPublicKeyAsync(pubkey);
        }

        /// <summary>
        /// Helper method used to send the public key to the dApp.
        /// Important at initialization.
        /// </summary>
        /// <param name="pubkey"></param>
        /// <returns></returns>
        /// <exception cref="WalletClientNotIntializedException"></exception>
        private static async Task SendPublicKeyAsync(string pubkey)
        {
            if (client is null)
            {
                throw new WalletClientNotIntializedException();
            }

            await client.EmitAsync(
                "connect_wallet",
                new PlutonicationMessage { Data = pubkey, Room = roomKey });
        }

        /// <summary>
        /// Sends payload's signature to the dApp.
        /// </summary>
        /// <param name="signerResult"></param>
        /// <returns></returns>
        /// <exception cref="WalletClientNotIntializedException"></exception>
        public static async Task SendPayloadSignatureAsync(SignerResult signerResult)
        {
            if (client is null)
            {
                throw new WalletClientNotIntializedException();
            }

            await client.EmitAsync(
                "payload_signature",
                new PlutonicationMessage { Data = signerResult, Room = roomKey });
        }

        /// <summary>
        /// Sends raw message's signature to the dApp.
        /// </summary>
        /// <param name="signerResult"></param>
        /// <returns></returns>
        /// <exception cref="WalletClientNotIntializedException"></exception>
        public static async Task SendRawSignatureAsync(SignerResult signerResult)
        {
            if (client is null)
            {
                throw new WalletClientNotIntializedException();
            }

            await client.EmitAsync(
                "raw_signature",
                new PlutonicationMessage { Data = signerResult, Room = roomKey });
        }

        /// <summary>
        /// Disconnects from the Plutonication server.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="WalletClientNotIntializedException"></exception>
        public static async Task DisconnectAsync()
        {
            if (client is null)
            {
                throw new WalletClientNotIntializedException();
            }

            await client.DisconnectAsync();
            client.Dispose();
        }

        /// <summary>
        /// Helper method that translates hex string to uint
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static uint HexStringToUint(string hex)
        {
            hex = hex.Replace("0x", ""); // remove the 0x if it's there
            if (uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result))
            {
                return result;
            }
            else
            {
                throw new FormatException("The provided string is not a valid hexadecimal number");
            }
        }
    }
}
