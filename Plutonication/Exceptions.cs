namespace Plutonication
{
    public class PlutonicationConnectionException : Exception
    {
        public PlutonicationConnectionException()
        {

        }
    }

    public class PlutonicationNotConnectedException : Exception
    {
        public PlutonicationNotConnectedException()
        {
        }
    }

    public class WalletClientNotIntializedException : Exception
    {
        public WalletClientNotIntializedException()
        {
        }
    }

    public class WrongMessageReceivedException : Exception
    {
        public WrongMessageReceivedException()
        {
        }
    }

    public class AccessCredentialsBadFormatException : Exception
    {
        public AccessCredentialsBadFormatException()
        {
        }

        public AccessCredentialsBadFormatException(string message)
            : base(message)
        {
        }
    }
}

