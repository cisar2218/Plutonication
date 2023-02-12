using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ajuna.NetApi.Model.Extrinsics;

namespace Plutonication
{
    public abstract class PlutoManager : IPlutoManager
    {
        protected const int DEFAULT_READSTREAM_TIMEOUT = 1000; // miliseconds
        protected TcpClient Client { get; set; }
        protected int Port { get; set; }
        protected IPAddress ServerAddress { get; set; }
        public abstract void CloseConnection();
        
        public PlutoMessage ReceiveMessage(int timeoutMiliseconds = DEFAULT_READSTREAM_TIMEOUT)
        {
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];

            stream.ReadTimeout = timeoutMiliseconds > 0 ? timeoutMiliseconds : DEFAULT_READSTREAM_TIMEOUT;
            Int32 bytes = stream.Read(data, 0, data.Length);
            if (!(bytes > 0))
            {
                throw new Exception(String.Format("Timeout ({0} ms). You can adjust timeout as {1} parameter {2}.",
                 stream.ReadTimeout, nameof(ReceiveMessage), nameof(timeoutMiliseconds)));
            }
            int customDataLenght = bytes-1;
            Byte[] customData = new Byte[customDataLenght]; 
            Array.Copy(data, 1, customData, 0, customDataLenght);
            return new PlutoMessage((MessageCode)data[0], customData);
        }
        public async Task<PlutoMessage> ReceiveMessageAsync()
        {
            // throw new NotImplementedException();
            // TODO implement and test properly
            NetworkStream stream = Client.GetStream();
            Byte[] data = new Byte[256];

            Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
            if (!(bytes > 0))
            {
                throw new Exception("No data received.");
            }
            int customDataLenght = bytes-1;
            Byte[] customData = new Byte[customDataLenght]; 
            Array.Copy(data, 1, customData, 0, customDataLenght);
            return new PlutoMessage((MessageCode)data[0], customData);
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

        public async Task SendMethodAsync(Method transaction)
        {
            Byte[] msg = new Byte[transaction.Parameters.Length + 2];

            msg[0] = transaction.ModuleIndex;
            msg[1] = transaction.CallIndex;
            transaction.Parameters.CopyTo(msg, 2);

            await SendMessageAsync(new PlutoMessage(MessageCode.Method, msg));
        }
        public void SendMethod(Method transaction)
        {
            Byte[] msg = new Byte[transaction.Parameters.Length + 2];

            msg[0] = transaction.ModuleIndex;
            msg[1] = transaction.CallIndex;
            transaction.Parameters.CopyTo(msg, 2);

            SendMessage(new PlutoMessage(MessageCode.Method, msg));
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

        public override string ToString()
        {
            return String.Format(
                "{0} <[{1}]{2}:[{3}]{4}>",
                nameof(PlutoManager),
                nameof(ServerAddress),
                ServerAddress.ToString(),
                nameof(Port),
                Port.ToString());
        }
    }
}