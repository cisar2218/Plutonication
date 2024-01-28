using System;
namespace Plutonication
{
    // Example payload:
    // [{"specVersion":"0x000003e0",
    // "transactionVersion":"0x00000001",
    // "address":"e8LwFkKFxDfvY4K52t7oyvbdApWGbVEP2Xk8PEoNuGG82M8",
    // "blockHash":"0x1e786fb92f81c804cf3a18fb2a5780d87048141f4add94ab2217e3773ada76c8",
    // "blockNumber":"0x003c37b4","era":"0x4401",
    // "genesisHash":"0x262e1b2ad728475fd6fe88e62d34c200abe6fd693931ddad144059b1eb884e5b",
    // "method":"0x0a070016b3861912eb2dda98ca3abc80f8b5b01b42c00753222dc5be9373117d2e616f0b00a0724e1809",
    // "nonce":"0x00000002",
    // "signedExtensions":["CheckNonZeroSender","CheckSpecVersion","CheckTxVersion","CheckGenesis","CheckMortality","CheckNonce","CheckWeight","ChargeTransactionPayment"],
    // "tip":"0x00000000000000000000000000000000","version":4}]
    
    public class Payload
    {
        public string specVersion { get; set; }
        public string transactionVersion { get; set; }
        public string address { get; set; }
        public string blockHash { get; set; }
        public string blockNumber { get; set; }
        public string era { get; set; }
        public string genesisHash { get; set; }
        public string method { get; set; }
        public string nonce { get; set; }
        public List<string> signedExtensions { get; set; }
        public string tip { get; set; }
        public uint version { get; set; }
    }
}

