using Newtonsoft.Json;
using SocketIOClient;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Primitive;

namespace Plutonication
{
    /// <summary>
    /// Custom implementation of <b>Substrate.NetApi.Model.Types.Account</b>
    /// that enables communication with Plutonication dApps.
    /// </summary>
    public class PlutonicationAccount : Account
    {
        private SocketIO client;

        private string roomKey = "";

        public static string PublicKey = "";

        private TaskCompletionSource<byte[]>? signature;

        public PlutonicationAccount(SocketIO client, string publicKey, string roomKey)
        {
            this.client = client;
            this.roomKey = roomKey;

            Create(KeyType.Sr25519, Utils.GetPublicKeyFrom(publicKey));

            client.On("raw_signature", signatureJson =>
            {
                SignerResult[]? signerResult = JsonConvert.DeserializeObject<SignerResult[]>(signatureJson.ToString());

                if (signature == null)
                {
                    return;
                }

                if (signerResult is null || !signerResult.Any() || signerResult[0].signature.Length < 2)
                {
                    throw new WrongMessageReceivedException();
                }

                signature.TrySetResult(Utils.HexToByteArray(signerResult[0].signature.Substring(2)));
            });

            client.On("payload_signature", signatureJson =>
            {
                SignerResult[]? signerResult = JsonConvert.DeserializeObject<SignerResult[]>(signatureJson.ToString());

                if (signature == null)
                {
                    return;
                }

                if (signerResult is null || !signerResult.Any() || signerResult[0].signature.Length < 4)
                {
                    throw new WrongMessageReceivedException();
                }

                signature.TrySetResult(Utils.HexToByteArray(signerResult[0].signature.Substring(4)));
            });
        }

        public override async Task<byte[]> SignAsync(byte[] message)
        {
            if (!client.Connected)
            {
                throw new PlutonicationNotConnectedException();
            }

            signature = new TaskCompletionSource<byte[]>();

            await client.EmitAsync(
                "sign_raw",
                new PlutonicationMessage
                {
                    Data = new RawMessage
                    {
                        address = Value,
                        data = Utils.Bytes2HexString(message).ToLower(),
                        type = "bytes",
                    },
                    Room = roomKey
                });

            return await signature.Task;
        }

        public override async Task<byte[]> SignPayloadAsync(Substrate.NetApi.Model.Extrinsics.Payload payload)
        {
            if (!client.Connected)
            {
                throw new PlutonicationNotConnectedException();
            }

            int chargeBytesPosition = 0;
            byte[] chargeBytes = payload.SignedExtension.Charge is ChargeTransactionPayment ?
                ((ChargeTransactionPayment)payload.SignedExtension.Charge).Encode() :
                ((ChargeAssetTxPayment)payload.SignedExtension.Charge).Encode();

            Payload data = new Payload
            {
                specVersion = Utils.Bytes2HexString(Utils.Value2Bytes(payload.SignedExtension.SpecVersion).Reverse().ToArray()).ToLower(),
                transactionVersion = Utils.Bytes2HexString(Utils.Value2Bytes(payload.SignedExtension.TxVersion).Reverse().ToArray()).ToLower(),
                address = this.Value,
                blockHash = Utils.Bytes2HexString(payload.SignedExtension.StartEra).ToLower(),
                blockNumber = Utils.Bytes2HexString(Utils.Value2Bytes(payload.SignedExtension.Mortality.Phase)).ToLower(),
                era = Utils.Bytes2HexString(payload.SignedExtension.Mortality.Encode()).ToLower(),
                genesisHash = Utils.Bytes2HexString(payload.SignedExtension.Genesis).ToLower(),
                method = Utils.Bytes2HexString(payload.Call.Encode()).ToLower(),
                nonce = Utils.Bytes2HexString(Utils.Value2Bytes((uint)(payload.SignedExtension.Nonce.Value)).Reverse().ToArray()).ToLower(),
                tip =
                payload.SignedExtension.Charge is ChargeTransactionPayment ?
                Utils.Bytes2HexString(
                    new U128(CompactInteger.Decode(chargeBytes).Value).Encode().Reverse().ToArray()
                ) :
                Utils.Bytes2HexString(
                    new U128(CompactInteger.Decode(((Memory<byte>)chargeBytes).Slice(chargeBytesPosition).ToArray(),
                    ref chargeBytesPosition).Value).Encode().Reverse().ToArray()
                ) + Utils.Bytes2HexString(
                    new U128(CompactInteger.Decode(((Memory<byte>)chargeBytes).Slice(chargeBytesPosition).ToArray(),
                    ref chargeBytesPosition).Value).Encode().Reverse().ToArray(),
                    Utils.HexStringFormat.Pure
                ),
            };

            signature = new TaskCompletionSource<byte[]>();

            await client.EmitAsync(
                "sign_payload",
                new PlutonicationMessage
                {
                    Data = data,
                    Room = roomKey
                });

            return await signature.Task;
        }

        /// <summary>
        /// Disconnects from the Plutonication server.
        /// </summary>
        /// <returns></returns>
        public async Task DisconnectAsync()
        {
            await client.DisconnectAsync();
            client.Dispose();
        }
    }
}

