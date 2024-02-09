using Newtonsoft.Json;
using SocketIOClient;

namespace Plutonication
{
	public class PlutonicationDAppClient
	{
        /// <summary>
        /// Initializes the Plutonication DApp client connection.
        /// </summary>
        /// <param name="ac">The credentials required for connecting to the Plutonication server.</param>
        /// <param name="onReceivePublicKey">Callback function to handle the received public key.</param>
        /// <returns>PlutonicationAccount ~ external wallet account.</returns>
        /// <exception cref="WrongMessageReceivedException">Error when receiving a wrong message from the Plutonication server.</exception>
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey
        ) {
            var client = new SocketIO(ac.Url);

            // Wait for the dApp socket client to connect.
            await client.ConnectAsync();

            // Create the room
            await client.EmitAsync(
                "create_room",
                new PlutonicationMessage { Data = "Nothing here", Room = ac.Key });

            // Wait for the wallet.
            // It needs to send to pubkey to this dApp client.
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

            // After receiving the publicKey, initialize the PlutonicationAccount.
            return new PlutonicationAccount(client, await publicKey.Task, ac.Key);
        }
    }
}
