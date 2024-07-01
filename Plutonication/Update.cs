using System;
using System.Collections.Generic;
using System.Text;

namespace Plutonication
{
    public class Update
    {
        public int Id { get; set; }
        public byte[] Status { get; set; } = new byte[0];
    }
}
