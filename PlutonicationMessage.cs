﻿using System;
namespace Plutonication
{
    public class PlutonicationMessage
    {
        public string Room { get; set; }
        public string Data { get; set; } = "";
    }
    public class PlutonicationSignedResult
    {
        public string Room { get; set; }
        public SignerResult? Data { get; set; }
    }
}

