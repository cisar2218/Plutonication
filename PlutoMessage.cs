using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonication
{
    public class PlutoMessage
    {
        public MessageCode Identifier { get; set; }
        public Byte[] CustomData {get;set;}
        public PlutoMessage(MessageCode id, String customData)
        {
            Identifier = id;
            CustomData = System.Text.Encoding.ASCII.GetBytes(customData);
        }
        public PlutoMessage(MessageCode id, Byte[] customData)
        {
            Identifier = id;
            CustomData = customData;
        }

        public String CustomDataToString() {
            return System.Text.Encoding.ASCII.GetString(CustomData, 0, CustomData.Length);
        }
        public Byte[] ToByteArray()
        {
            Byte[] merged = new Byte[CustomData.Length+1];
            merged[0] = (byte)Identifier;
            CustomData.CopyTo(merged, 1);
            return merged;
        }
    }
}