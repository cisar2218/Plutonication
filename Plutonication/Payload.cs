namespace Plutonication
{
    /// <summary>
    /// Representation of the Polkadot.js api SignerPayloadJSON type
    /// </summary>
    public class Payload
    {
        public string? specVersion { get; set; }
        public string? transactionVersion { get; set; }
        public string? address { get; set; }
        public string? blockHash { get; set; }
        public string? blockNumber { get; set; }
        public string? era { get; set; }
        public string? genesisHash { get; set; }
        public string? method { get; set; }
        public string? nonce { get; set; }
        public List<string>? signedExtensions { get; set; }
        public string? tip { get; set; }
        public uint version { get; set; }
    }
}

