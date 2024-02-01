using SocketIOClient;

namespace Plutonication
{
	public class PlutonicationWalletClient
	{
		public static SocketIO? Client;

        public static string RoomKey = "";

		public static async Task InitializeAsync(
            AccessCredentials ac,
            string pubkey,
            Action<SocketIOResponse> signPayload,
            Action<SocketIOResponse> signRaw)
		{
            RoomKey = ac.Key;
            Console.WriteLine(ac.Url);
			Client = new SocketIO(ac.Url);

            Client.On("sign_payload", signPayload);

            Client.On("sign_raw", signRaw);


            await Client.ConnectAsync();

            Console.WriteLine("Plutonication connected");

            await SendPublicKeyAsync(pubkey);
        }

		private static async Task SendPublicKeyAsync(string pubkey)
		{
            if (Client is null){
                throw new WalletClientNotIntializedException();
            }

            await Client.EmitAsync(
                "pubkey",
                new PlutonicationMessage { Data = pubkey, Room = RoomKey });
        }

        public static async Task DisconnectAsync()
        {
            if (Client is null){
                throw new WalletClientNotIntializedException();
            }

            await Client.DisconnectAsync();
            Client.Dispose();
        }

        public static async Task SendPayloadSignatureAsync(SignerResult signerResult)
        {
            if (Client is null){
                throw new WalletClientNotIntializedException();
            }

            await Client.EmitAsync(
                "payload_signature",
                new PlutonicationMessage { Data = signerResult, Room = RoomKey });
        }

        public static async Task SendRawSignatureAsync(SignerResult signerResult)
        {
            if (Client is null){
                throw new WalletClientNotIntializedException();
            }

            await Client.EmitAsync(
                "raw_signature",
                new PlutonicationMessage { Data = signerResult, Room = RoomKey });
        }
    }
}

