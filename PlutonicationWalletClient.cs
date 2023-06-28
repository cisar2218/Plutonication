using System;
using System.Net.Sockets;
using SocketIOClient;

namespace Plutonication
{
	public class PlutonicationWalletClient
	{
		public static SocketIO? Client;

		public static async Task InitializeAsync(
            AccessCredentials ac,
            string pubkey,
            Action<SocketIOResponse> signPayload)
		{
            Console.WriteLine(ac.Url);
			Client = new SocketIO(ac.Url);

            Client.On("sign_payload", signPayload);

            await Client.ConnectAsync();

            Console.WriteLine("Connected");

			await SendPublicKeyAsync(pubkey);
        }

		public static async Task SendPublicKeyAsync(string pubkey)
		{
            await Client.EmitAsync(
                "pubkey",
                new PlutonicationMessage { Data = pubkey });
        }

        public static async Task DisconnectAsync()
        {
            await Client.DisconnectAsync();
            Client.Dispose();
        }

        public static async Task SendSignedPayloadAsync(SignerResult signerResult)
        {
            await Client.EmitAsync(
                "signed_payload",
                new PlutonicationSignedResult { Data = signerResult });
        }
    }
}

