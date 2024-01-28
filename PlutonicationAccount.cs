using System;
using Newtonsoft.Json;
using SocketIOClient;
using Substrate.NetApi;
using Substrate.NetApi.Model.Extrinsics;
using Substrate.NetApi.Model.Types;

namespace Plutonication
{
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

            client.On("raw_signature", signatureJson =>
            {
                SignerResult signerResult = JsonConvert.DeserializeObject<SignerResult[]>(signatureJson.ToString())[0];

                if (signature == null)
                {
                    return;
                }

                signature.TrySetResult(Utils.HexToByteArray(signerResult.signature));
            });
        }

        public override async Task<byte[]> SignRawAsync(byte[] message)
        {
            if (!client.Connected)
            {
                throw new Exception();
            }

            await client.EmitAsync(
                "sign_raw",
                new PlutonicationMessage { Data = Utils.Bytes2HexString(message).ToLower(), Room = roomKey });

            signature = new TaskCompletionSource<byte[]>();

            return await signature.Task;
        }

        public async Task DisconnectAsync()
        {
            await client.DisconnectAsync();
            client.Dispose();
        }
    }
}

