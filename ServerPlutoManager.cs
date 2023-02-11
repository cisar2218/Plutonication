using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public class ServerPlutoManager : PlutoManager
    {
        private TcpListener server;
        public ServerPlutoManager(int port)
        {
            Port = port;
        }

        public void SendTransaction(Byte palletIdx, Byte callIdx, Byte[] parameters)
        {
            NetworkStream stream = Client.GetStream();
            Byte[] msg = new Byte[parameters.Length + 2];
            msg[0] = palletIdx;
            msg[1] = callIdx;
            Array.Copy(parameters, 0, msg, 2, parameters.Length);
            stream.Write(msg, 0, msg.Length);
        }

        public PlutoMessage ReceiveMessage()
        {
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];

            Int32 bytes = stream.Read(data, 0, data.Length);
            String responseData = System.Text.Encoding.ASCII.GetString(data, 1, bytes - 1);

            PlutoMessage message = new PlutoMessage((MessageCode)data[0], responseData);
            return message;
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
        public override void CloseConnection()
        {
            Client.Close();
            server.Stop();
        }
    }
}