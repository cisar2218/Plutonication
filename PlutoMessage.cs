using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonication
{
    public class PlutoMessage
    {
        public MessageCode Identifier { get; set; }
        public String CustomData { get; set; }
        public PlutoMessage(MessageCode id, String customData)
        {
            Identifier = id;
            CustomData = customData;
        }
        public Byte[] CustomDataAsBytes()
        {
            return System.Text.Encoding.ASCII.GetBytes(CustomData);
        }
        public Byte[] ToByteArray()
        {
            Byte[] targetArray = new Byte[CustomData.Length + 1];
            targetArray[0] = (byte)Identifier;
            Array.Copy(CustomDataAsBytes(), 0, targetArray, 1, CustomData.Length);
            return targetArray;
        }
    }
}