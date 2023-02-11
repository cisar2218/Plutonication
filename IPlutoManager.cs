using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ajuna.NetApi.Model.Extrinsics;

namespace Plutonication
{
    public interface IPlutoManager
    {
        void SendMessage(PlutoMessage message);
        Task SendMessageAsync(MessageCode code);

        void SendMethod(Method transaction);
        Task SendMethodAsync(Method transaction);
        Task<PlutoMessage> ReceiveMessageAsync();
        PlutoMessage ReceiveMessage();
    }
}