using System;
using System.Net.Sockets;
using SocketIOClient;

namespace Plutonication
{
	public class PlutonicationWalletClient
	{
		public static SocketIO? Client;

		public static async Task InitializeAsync(AccessCredentials c, string pubkey)
		{
			Client = new SocketIO(c.Url);

            await Client.ConnectAsync();

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
    }
}

