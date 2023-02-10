using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonication
{
    public interface IPlutoManager
    {
        public void SendMessage(string data);
        public String ReceiveMessage();
        public void CloseConnection();
    }
}