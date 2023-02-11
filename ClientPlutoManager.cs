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

        public (Byte, Byte, Byte[]) ReceiveTransaction()
        {
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];
            Int32 bytes = stream.Read(data, 0, data.Length);
            return (data[0], data[1], data.Skip(2).ToArray());
        }

        public void SendMessage(PlutoMessage message)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = message.ToByteArray();
            stream.Write(msg, 0, msg.Length);
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