using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonication
{
    public interface IPlutoManager
    {
        void SendMessage(PlutoMessage message);
        Task SendMessageAsync(MessageCode code);

        Task<PlutoMessage> ReceiveMessageAsync();
        PlutoMessage ReceiveMessage();
    }
}