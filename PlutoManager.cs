using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public abstract class PlutoManager : IPlutoManager
    {
        protected TcpClient Client {get; set;}
        protected int Port { get; set; }
        protected IPAddress ServerAddress { get; set; }
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

        public void SendMessage(string data)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);
        }
        public abstract void CloseConnection();
    }
}