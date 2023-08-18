using System;
using System.Security.Principal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schnorrkel.Signed;
using SocketIOClient;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Rpc;
using Substrate.NetApi.Model.Types;
using Substrate.NetApi.Model.Types.Base;

namespace Plutonication
{
	public class PlutonicationDAppClient
	{
        public static SocketIO? Client;

        public static string RoomKey = "";

        public static string PublicKey = "";

        public static SubstrateClient SubstrateClient;

        public static UnCheckedExtrinsic LastExtrinsic;

        public static async Task InitializeAsync(
            AccessCredentials ac,
            Action<string> receivePublicKey,
            SubstrateClient substrateClient
            )
        {
            RoomKey = ac.Key;
            Console.WriteLine(ac.Url);
            Client = new SocketIO(ac.Url);

            SubstrateClient = substrateClient;

            Client.On("pubkey", publicKeyJson =>
            {
                string pubkey = JsonConvert.DeserializeObject<string[]>(publicKeyJson.ToString())[0];

                PublicKey = pubkey;

                receivePublicKey.Invoke(pubkey);
            });

            Client.On("signed_payload", signatureJson =>
            {
                if (!substrateClient.IsConnected) {
                    return;
                }

                SignerResult signerResult = JsonConvert.DeserializeObject<SignerResult[]>(signatureJson.ToString())[0];

                LastExtrinsic.AddPayloadSignature(Utils.HexToByteArray(signerResult.signature));

                substrateClient.Author.SubmitExtrinsicAsync(Utils.Bytes2HexString(LastExtrinsic.Encode()));
            });


            await Client.ConnectAsync();

            Console.WriteLine("Plutonication connected");
        }

        public static async Task SendPayloadAsync(Method method, Era era,
            uint nonce, ChargeType charge, Hash genesis, Hash startEra, RuntimeVersion runtime)
        {
            var uncheckedExtrinsic =
                new UnCheckedExtrinsic(true, null, method, era, nonce, charge, genesis, startEra);

            var payload = uncheckedExtrinsic.GetPayload(runtime).Encode();

            /// Payloads longer than 256 bytes are going to be `blake2_256`-hashed.
            if (payload.Length > 256) payload = HashExtension.Blake2(payload, 256);

            await Client.EmitAsync(
                "sign_raw",
                new PlutonicationMessage { Data = Utils.Bytes2HexString(payload).ToLower(), Room = RoomKey });

            // .ToLower can be removed in the newer version
        }

        public static async Task SendPayloadAsync(byte palletIndex, byte callIndex, byte[] parameters,
            uint lifeTime = 64)
        {
            var token = CancellationToken.None;
            Method method = new Method(palletIndex, callIndex, parameters);

            var nonce = await SubstrateClient.System.AccountNextIndexAsync(PublicKey, token);

            Era era;
            Hash startEra;

            if (lifeTime == 0)
            {
                era = Era.Create(0, 0);
                startEra = SubstrateClient.GenesisHash;
            }
            else
            {
                startEra = await SubstrateClient.Chain.GetFinalizedHeadAsync(token);
                var finalizedHeader = await SubstrateClient.Chain.GetHeaderAsync(startEra, token);
                era = Era.Create(lifeTime, finalizedHeader.Number.Value);
            }

            var uncheckedExtrinsic =
                new UnCheckedExtrinsic(true, null, method, era, nonce, ChargeTransactionPayment.Default(), SubstrateClient.GenesisHash, startEra);

            var payload = uncheckedExtrinsic.GetPayload(SubstrateClient.RuntimeVersion).Encode();

            /// Payloads longer than 256 bytes are going to be `blake2_256`-hashed.
            if (payload.Length > 256) payload = HashExtension.Blake2(payload, 256);

            await Client.EmitAsync(
                "sign_raw",
                new PlutonicationMessage { Data = Utils.Bytes2HexString(payload).ToLower(), Room = RoomKey });

            // .ToLower can be removed in the newer version
        }

        public static async Task DisconnectAsync()
        {
            await Client.DisconnectAsync();
            Client.Dispose();
        }
    }
}

