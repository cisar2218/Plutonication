namespace Plutonication
{
    public class PlutonicationNotConnectedException : Exception
    {
        public PlutonicationNotConnectedException()
        {
        }

        public PlutonicationNotConnectedException(string message)
            : base(message)
        {
        }

        public PlutonicationNotConnectedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class WalletClientNotIntializedException : Exception
    {
        public WalletClientNotIntializedException()
        {
        }

        public WalletClientNotIntializedException(string message)
            : base(message)
        {
        }

        public WalletClientNotIntializedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class WrongMessageReceivedException : Exception
    {
        public WrongMessageReceivedException()
        {
        }

        public WrongMessageReceivedException(string message)
            : base(message)
        {
        }

        public WrongMessageReceivedException(string message, Exception inner)
            : base(message, inner)
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

        public AccessCredentialsBadFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

