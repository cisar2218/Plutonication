using System;

namespace Plutonication
{
    public class ConnectionExpetion : Exception
    {
        public ConnectionExpetion()
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

