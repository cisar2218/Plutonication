using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public delegate void Notify();
    public class PlutoEventManager : PlutoManager, IPlutoManager
    {
        public event Notify ConnectionEstablished;
        public event Notify ConnectionRefused;
        public event Notify ConnectionClosed;
        public event Notify MessageReceived;

        private bool loopIsReceiving = true;
        private TcpListener server;
        public Queue<PlutoMessage> IncomingMessages = new Queue<PlutoMessage>();
        public int MESSAGE_QUEUE_CAPACITY = 20;


        public async override Task ConnectSafeAsync(IPAddress iPAddress, int port, string key, int timeoutMiliseconds = 60_000) {
            
        }
        public async Task ConnectSafeAsync(AccessCredentials c, int timeoutMiliseconds = 60_000)
        {
            Client = new TcpClient(c.Address, c.Port);
            var authMessage = new PlutoMessage(MessageCode.Auth, c.Key);
            var sendingMsg = SendMessageAsync(authMessage);
            // 1 minute delay
            try
            {
                var recvSuccess = await ReceiveMessageAsync(timeoutMiliseconds);
                if (recvSuccess.Identifier != MessageCode.Success)
                {
                    throw new Exception(String.Format("Connection wasn't established. Response {0} is {1}",
                                        nameof(MessageCode), recvSuccess.Identifier));
                }
                ConnectionEstablished?.Invoke();
            }
            catch
            {
                throw;
            }
        }

        public void StopReceiveLoop() {
            loopIsReceiving = false;
        }

        public async Task SetupReceiveLoopAsync() {
            loopIsReceiving = true;
            while (loopIsReceiving) {
                try {
                    PlutoMessage incMessage = await ReceiveMessageAsync(10_000);
                    if (IncomingMessages.Count() >= MESSAGE_QUEUE_CAPACITY) {
                        Task sendingMsg = SendMessageAsync(MessageCode.FilledOut);
                    }
                    IncomingMessages.Enqueue(incMessage);
                    MessageReceived?.Invoke();
                } catch { /* no incoming messages */}
            }
        }

        public async Task ListenSafeAsync(string key,int port, int timeoutMiliseconds = 60_000)
        {
            server = new TcpListener(port);
            server.Start();
            Client = server.AcceptTcpClient();
            
            PlutoMessage authMessage = await ReceiveMessageAsync(timeoutMiliseconds);
            Task sendingResponse;
            Console.WriteLine(authMessage.Identifier);
            Console.WriteLine(authMessage.CustomDataToString());
            if (authMessage.Identifier == MessageCode.Auth && authMessage.CustomDataToString() == key) {
                sendingResponse = SendMessageAsync(MessageCode.Success);
                ConnectionEstablished?.Invoke();
            } else {
                sendingResponse = SendMessageAsync(MessageCode.Refused);
                ConnectionRefused?.Invoke();
            }
            await sendingResponse;
        }
        public bool IsConnected()
        {
            return Client != null && Client.Connected;
        }
        public override void CloseConnection()
        {
            if (IsConnected()) {
                Client.Close();
            } 
            server?.Stop();
        }
    }
}