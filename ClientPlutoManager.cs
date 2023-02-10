using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ClientPlutoManager : PlutoManager, IPlutoManager
    {
        public ClientPlutoManager(IPAddress serverAddress, int port)
        {
            ServerAddress = serverAddress;
            Port = port;
        }

        public void Connect()
        {
            int port = 8080;
            string serverAddr = "127.0.0.1";
            Client = new TcpClient(serverAddr, port);
        }

        public override void CloseConnection()
        {
            Client.Close();
        }
    }
}