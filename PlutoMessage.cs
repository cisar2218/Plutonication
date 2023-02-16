using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Ajuna.NetApi.Model.Extrinsics;

namespace Plutonication
{
    public class PlutoMessage
    {

        public MessageCode Identifier { get; set; }
        public Byte[] CustomData { get; set; }
       
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

        public String CustomDataToString()
        {
            return System.Text.Encoding.ASCII.GetString(CustomData, 0, CustomData.Length);
        }
        public Byte[] ToByteArray()
        {
            Byte[] merged = new Byte[CustomData.Length + 1];
            merged[0] = (byte)Identifier;
            CustomData.CopyTo(merged, 1);
            return merged;
        }


        public Method GetMethod()
        {
            if (Identifier != MessageCode.Method)
            {
                throw new Exception(String.Format(
                    "Can't convert cause '{0}' code is not suited for {1}.", nameof(Identifier), MessageCode.Method));
            }
            var data = new Byte[0];
            if (CustomData.Length > 2)
            {
                data = CustomData.Skip(2).Take(CustomData.Length - 2).ToArray();
            }
            return new Method(CustomData[0], CustomData[1], data);
        }
    }
}