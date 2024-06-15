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
        /// <param name="socketIOOptions">Extra configuration for the WebSocketClient</param>
        /// <param name="onDisconnected">Callback function to handle the disconnection from the Plutonication Server.</param>
        /// <param name="onReconnectAttempt">Callback function to handle the reconnection attempt to the Plutonication Server.</param>
        /// <param name="onReconnected">Callback function to handle the reconnection to the Plutonication Server.</param>
        /// <param name="onReconnectFailed">Callback function to handle the reconnect failed to the Plutonication Server.</param>
        /// <param name="onWalletDisconnected">Callback function to handle the disconnection of the respective Wallet.</param>
        /// <returns>PlutonicationAccount ~ external wallet account.</returns>
        /// <exception cref="PlutonicationConnectionException">Error when unable to establish connection with the websocket server provided in the access credentials.</exception>
        /// <exception cref="WrongMessageReceivedException">Error when receiving a wrong message from the Plutonication server.</exception>
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey,
            SocketIOOptions? socketIOOptions = null,
            EventHandler<string>? onDisconnected = null,
            EventHandler<int>? onReconnectAttempt = null,
            EventHandler<int>? onReconnected = null,
            EventHandler? onReconnectFailed = null,
            Action? onWalletDisconnected = null
        )
        {
            socketIOOptions ??= new SocketIOOptions();

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

            // Connect to the room
            await client.EmitAsync(
                "connect_dapp",
                new PlutonicationMessage { Data = null, Room = ac.Key });

            // Wait for the wallet.
            // It needs to send to pubkey to this dApp client.
            var publicKey = new TaskCompletionSource<string>();

            client.On("pubkey", receivedPublicKey =>
            {
                string[]? pubkey = JsonConvert.DeserializeObject<string[]>(receivedPublicKey.ToString());

                if (pubkey is null || !pubkey.Any())
                {
                    throw new WrongMessageReceivedException();
                }

                Task confirmDAppConnection = client.EmitAsync(
                    "confirm_dapp_connection",
                    new PlutonicationMessage { Data = null, Room = ac.Key });

                publicKey.TrySetResult(pubkey[0]);

                onReceivePublicKey.Invoke(pubkey[0]);
            });

            //  Handle the Wallet disconnection
            client.On("disconnect", _ =>
            {
                onWalletDisconnected?.Invoke();
            });

            // Handle client disconnection
            client.OnDisconnected += onDisconnected;
            client.OnReconnectAttempt += onReconnectAttempt;
            client.OnReconnected += onReconnected;
            client.OnReconnectFailed += onReconnectFailed;

            // After receiving the publicKey, initialize the PlutonicationAccount.
            return new PlutonicationAccount(client, await publicKey.Task, ac.Key);
        }
    }
}
