using System.Net.Sockets;
using System.Net;

namespace Plutonication;
public class ConnectionManager
{
    public static Socket Connect(IPAddress address, int port)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endpoint = new IPEndPoint(address, port);

        try
        {
            socket.Connect(endpoint);
            return socket;
        }
        catch (SocketException ex)
        {
            Console.WriteLine("Failed to connect: " + ex.Message);
            return null;
        }
    }
    public static Socket Listen(IPAddress ipLocal, int port)
    {
        string ipStr = ipLocal.ToString();
        IPEndPoint localEndPoint = new IPEndPoint(ipLocal, port);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(localEndPoint);
        listener.Listen();

        return listener;
    }

    public static string GetMyWebSocketLink(int port)
    {
        string hostName = Dns.GetHostName();
        IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
        IPAddress localIP = ipAddresses[0];
        return "ws://" + localIP.ToString() + ":" + port;
    }
}
