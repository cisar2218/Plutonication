using System;
namespace Plutonication
{
	public class SignerResult
	{
		public uint Id { get; set; }

		// Must be in hex
		public string Signature { get; set; } = "0x";
	}
}

