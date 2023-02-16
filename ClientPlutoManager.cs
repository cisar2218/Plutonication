using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ClientPlutoManager : PlutoManager
    {
        public ClientPlutoManager(IPAddress serverAddress, int port)
        {
            ServerAddress = serverAddress;
            Port = port;
        }

        public void Connect()
        {
            Client = new TcpClient(ServerAddress.ToString(), Port);
        }

        public bool IsConnected() {
            return Client != null && Client.Connected;
        }

        public override void CloseConnection()
        {
            Client.Close();
        }
    }
}