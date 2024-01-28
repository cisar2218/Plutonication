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
        public static async Task<PlutonicationAccount> InitializeAsync(
            AccessCredentials ac,
            Action<string> onReceivePublicKey,
            SubstrateClient substrateClient
            )
        {
            var client = new SocketIO(ac.Url);

            await client.ConnectAsync();

            await client.EmitAsync(
                "create_room",
                new PlutonicationMessage { Data = "Nothing here", Room = ac.Key });

            var publicKey = new TaskCompletionSource<string>();

            client.On("pubkey", receivedPublicKey =>
            {
                string pubkey = JsonConvert.DeserializeObject<string[]>(receivedPublicKey.ToString())[0];

                publicKey.TrySetResult(pubkey);

                onReceivePublicKey.Invoke(pubkey);
            });

            return new PlutonicationAccount(client, await publicKey.Task, ac.Key);
        }
    }
}

