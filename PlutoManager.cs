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
        protected TcpClient Client { get; set; }
        protected int Port { get; set; }
        protected IPAddress ServerAddress { get; set; }
        public abstract void CloseConnection();

        public PlutoMessage ReceiveMessage()
        {
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];

            Int32 bytes = stream.Read(data, 0, data.Length);
            String responseData = System.Text.Encoding.ASCII.GetString(data, 1, bytes - 1);

            return new PlutoMessage((MessageCode)data[0], responseData);
        }

        public void SendMessage(PlutoMessage message)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = message.ToByteArray();
            stream.Write(msg, 0, msg.Length);
        }

        public void SendMessage(MessageCode code)
        {
            SendMessage(new PlutoMessage(code, String.Empty));
        }

        public static IPAddress GetMyIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
            var ip = ipAddresses.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Where(x =>
            {
                var nums = x.ToString().Split(".");
                int first = Int32.Parse(nums[0]);
                int second = Int32.Parse(nums[1]);
                return (
                    first == 192 && second == 168)
                || (first == 172 && ((second >= 16) && (second <= 31))
                );
            }).FirstOrDefault();
            return ip;
        }
    }
}