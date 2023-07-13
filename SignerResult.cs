using System;
namespace Plutonication
{
	public class SignerResult
	{
		public uint id { get; set; }

		// Must be in hex
		public string signature { get; set; } = "0x";
	}
}

