using System;
namespace Plutonication
{
    public class PlutonicationMessage
    {
        public int Room { get; set; }
        public string Data { get; set; } = "";
    }
    public class PlutonicationSignedResult
    {
        public int Room { get; set; }
        public SignerResult? Data { get; set; }
    }
}

