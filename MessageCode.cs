namespace Plutonication
{
    public enum MessageCode : Byte
    {
        PublicKey = 0,
        Success = 1,
        Refused = 2,
        Method = 3,
        Auth = 4,
        FilledOut = 5,
    }
}