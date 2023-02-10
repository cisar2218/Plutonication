using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ClientPlutoManager : IPlutoManager
    {
        private int Port { get; set; }
        private IPAddress ServerAddress { get; set; }
        private TcpClient Client { get; set; }
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

        public void SendMessage(string data)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);
        }

        public string ReceiveMessage()
        {
            NetworkStream stream = Client.GetStream();

            Byte[] data = new Byte[256];

            Int32 bytes = stream.Read(data, 0, data.Length);
            String responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            // TODO remove log
            Console.WriteLine("Received: {0}", responseData);
            return responseData;
        }

        public void CloseConnection()
        {
            Client.Close();
        }
    }
}