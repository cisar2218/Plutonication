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
            return ReceiveMessageAsync().GetAwaiter().GetResult();
        }
        public async Task<PlutoMessage> ReceiveMessageAsync()
        {
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];

            Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
            if (!(bytes > 0))
            {
                throw new Exception("No data received.");
            }
            String responseData = System.Text.Encoding.ASCII.GetString(data, 1, bytes - 1);
            return new PlutoMessage((MessageCode)data[0], responseData);
        }

        public void SendMessage(PlutoMessage message)
        {
            SendMessageAsync(message).GetAwaiter().GetResult();
        }

        public void SendMessage(MessageCode code)
        {
            SendMessageAsync(code).GetAwaiter().GetResult();
        }
        public async Task SendMessageAsync(MessageCode code)
        {
            await SendMessageAsync(new PlutoMessage(code, String.Empty));
        }
         public async Task SendMessageAsync(PlutoMessage message)
        {
            NetworkStream stream = Client.GetStream();
            byte[] msg = message.ToByteArray();
            await stream.WriteAsync(msg, 0, msg.Length);
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