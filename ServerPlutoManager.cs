using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ajuna.NetApi.Model.Extrinsics;

namespace Plutonication
{
    public class ServerPlutoManager : PlutoManager
    {
        private TcpListener server;
        public ServerPlutoManager(int port)
        {
            Port = port;
            ServerAddress = IPAddress.Parse("127.0.0.1");
        }
        public async Task SendTransactionAsync(Method transaction)
        {
            Byte[] msg = new Byte[transaction.Parameters.Length + 2];

            msg[0] = transaction.ModuleIndex;
            msg[1] = transaction.CallIndex;
            transaction.Parameters.CopyTo(msg, 2);

            await SendMessageAsync(new PlutoMessage(MessageCode.Method, msg));
        }
        public void SendTransaction(Method transaction)
        {
            SendTransactionAsync(transaction).GetAwaiter().GetResult();
        }

        public void AcceptClient()
        {
            Client = server.AcceptTcpClient();
        }
        public void StartServer()
        {
            try
            {
                server = new TcpListener(8080);
                server.Start();
            }
            catch
            {
                throw;
            }
        }

        private IPAddress GetServerLanIp()
        {

            throw new NotImplementedException();
        }
        public override void CloseConnection()
        {
            Client.Close();
            server.Stop();
        }
    }
}