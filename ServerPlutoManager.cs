using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ServerPlutoManager : IPlutoManager
    {
        private TcpListener server;
        private TcpClient Client;
        private int Port { get; set; }
        public ServerPlutoManager(int port)
        {
            Port = port;
        }
        public void SendMessage(string data)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);

            // TODO remove log
            Console.WriteLine("Sent: {0}", data);
        }

        public String ReceiveMessage() {
            NetworkStream stream = Client.GetStream();
            Byte[] rcv_data = new Byte[256];
            int i;
            while ((i = stream.Read(rcv_data, 0, rcv_data.Length)) != 0)
            {
                String responseData = System.Text.Encoding.ASCII.GetString(rcv_data, 0, i);
                // recieve transaction data
                // TODO remove log
                Console.WriteLine("Received: {0} {1}", responseData, i);
                return responseData;
            }
            return String.Empty; // when no data received
        }

        public String SendAndReadResponse() {
            return String.Empty;   
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
        public void CloseConnection() {
            Client.Close();
            server.Stop();
        }
    }
}