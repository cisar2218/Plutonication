using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ServerPlutoManager : PlutoManager, IPlutoManager
    {
        private TcpListener server;
        public ServerPlutoManager(int port)
        {
            Port = port;
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
        public override void CloseConnection() {
            Client.Close();
            server.Stop();
        }
    }
}