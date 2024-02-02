using Newtonsoft.Json;
using SocketIOClient;

namespace Plutonication
{
	public class PlutonicationDAppClient
	{
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey
        ) {
            var client = new SocketIO(ac.Url);

            await client.ConnectAsync();

            await client.EmitAsync(
                "create_room",
                new PlutonicationMessage { Data = "Nothing here", Room = ac.Key });

            var publicKey = new TaskCompletionSource<string>();

            client.On("pubkey", receivedPublicKey =>
            {
                string[]? pubkey = JsonConvert.DeserializeObject<string[]>(receivedPublicKey.ToString());
                
                if (pubkey is null || !pubkey.Any()){
                    throw new WrongMessageReceivedException();
                }

                publicKey.TrySetResult(pubkey[0]);

                onReceivePublicKey.Invoke(pubkey[0]);
            });

            return new PlutonicationAccount(client, await publicKey.Task, ac.Key);
        }
    }
}

