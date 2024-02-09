namespace Plutonication
{
    /// <summary>
    /// Representation of the Polkadot.js api SignerPayloadRaw type
    /// </summary>
	public class RawMessage
	{
		public string? address { get; set; }
        public string? data { get; set; }
        public string? type { get; set; }
    }
}
