using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Plutonication
{
    public abstract class PlutoManager
    {
        protected TcpClient Client {get; set;}
        protected int Port { get; set; }
        protected IPAddress ServerAddress { get; set; }
        public abstract void CloseConnection();
    }
}