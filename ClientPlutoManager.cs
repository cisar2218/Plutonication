using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ajuna.NetApi.Model.Extrinsics;

namespace Plutonication
{
    public class ClientPlutoManager : PlutoManager
    {
        public ClientPlutoManager(IPAddress serverAddress, int port)
        {
            ServerAddress = serverAddress;
            Port = port;
        }

        public Method ReceiveTransaction()
        {
            const int NUM_OF_BYTES_REQUIRED = 3;
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];
            Int32 bytes = stream.Read(data, 0, data.Length);
            if (data[0] != (byte)MessageCode.Method)
            {
                throw new Exception(String.Format("{0} not {1} of value {2}. Current value: {3}",
                    nameof(MessageCode), nameof(MessageCode.Method), ((int)MessageCode.Method), data[0]));
            }
            if (bytes > NUM_OF_BYTES_REQUIRED)
            {
                return new Method(data[1], data[2], data.Skip(3).ToArray());
            }
            else if (bytes == NUM_OF_BYTES_REQUIRED)
            {
                // No parrameters of Method => pass empty array
                return new Method(data[1], data[2], new Byte[0]);
            }
            else
            {
                throw new Exception(String.Format(
                    "Not enought bytes received to correspond Method format. Bytes Received: {0}, num of bytes requered: {1}",
                 bytes, NUM_OF_BYTES_REQUIRED));
            }
        }

        public void Connect()
        {
            int port = 8080;
            Client = new TcpClient(ServerAddress.ToString(), port);
        }

        public override void CloseConnection()
        {
            Client.Close();
        }
    }
}