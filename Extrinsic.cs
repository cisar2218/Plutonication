using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Plutonication
{
    [Serializable]
    public struct Extrinsic
    {
        public string method = String.Empty;
        public object parameters;
        public Extrinsic(string method, object parameters)
        {
            this.method = method;
            this.parameters = parameters;
        }

        public Extrinsic(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter br = new BinaryFormatter();
                this = (Extrinsic)br.Deserialize(ms);
            }
        }

        public byte[] Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                return stream.ToArray();
            }
        }
    }
}