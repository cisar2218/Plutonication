using Newtonsoft.Json;
using SocketIOClient;
using System;

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
        /// <exception cref="PlutonicationConnectionException">Error when unable to establish connection with the websocket server provided in the access credentials.</exception>
        /// <exception cref="WrongMessageReceivedException">Error when receiving a wrong message from the Plutonication server.</exception>
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey
        )
        {
            return await InitializeAsync(
                ac,
                onReceivePublicKey,
                new SocketIOOptions()
            );
        }

        /// <summary>
        /// Initializes the Plutonication DApp client connection.
        /// </summary>
        /// <param name="ac">The credentials required for connecting to the Plutonication server.</param>
        /// <param name="onReceivePublicKey">Callback function to handle the received public key.</param>
        /// <param name="socketIOOptions">Extra configuration for the WebSocketClient</param>
        /// <returns>PlutonicationAccount ~ external wallet account.</returns>
        /// <exception cref="PlutonicationConnectionException">Error when unable to establish connection with the websocket server provided in the access credentials.</exception>
        /// <exception cref="WrongMessageReceivedException">Error when receiving a wrong message from the Plutonication server.</exception>
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey,
            SocketIOOptions socketIOOptions
        ) {
            var client = new SocketIO(ac.Url, socketIOOptions);

            // Wait for the dApp socket client to connect.
            try
            {
                await client.ConnectAsync();
            }
            catch (SocketIOClient.ConnectionException)
            {
                throw new PlutonicationConnectionException();
            }

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
