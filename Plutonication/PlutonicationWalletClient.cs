using System.Globalization;
using System.Reflection;
using System.Security.Principal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Transport;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Rpc;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;
using System;

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
        /// <returns></returns>
        /// <exception cref="WrongMessageReceivedException">Error when receiving a wrong message from the Plutonication server.</exception>
		public static async Task InitializeAsync(
            AccessCredentials ac,
            string pubkey,
            Func<UnCheckedExtrinsic, RuntimeVersion, Task> signPayload,
            Func<RawMessage, Task> signRaw)
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
                    throw new WrongMessageReceivedException();
                }

                Task signRawTask = signRaw.Invoke(rawMessages[0]);
            });

            await client.ConnectAsync();

            await SendPublicKeyAsync(pubkey);
        }

        /// <summary>
        /// Helper method used to send the public key to the dApp.
        /// Important at initialisation.
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
                "pubkey",
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

