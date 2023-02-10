using System.Net.Sockets;
using System.Net;

namespace Plutonication;
public class ConnectionManager
{
    public static TcpClient? Connect(string serverAddr, int port)
    {
        try
        {
            TcpClient client = new TcpClient(serverAddr, port);
            Console.WriteLine("Client connected to " + serverAddr + ":" + port);
            return client;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to connect: " + ex.Message);
            return null;
        }
    }

    public static TcpListener? ListenLocal() {
        const int port = 8080;
        return Listen(GetMyIpAddress(), port);
    }
    public static TcpListener? Listen(IPAddress ipLocal, int port)
    {
        string ipStr = ipLocal.ToString();
        try
        {
            TcpListener listener = new TcpListener(port);
            return listener;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public static string GetMyWebSocketLink(int port)
    {
        string hostName = Dns.GetHostName();
        IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
        IPAddress localIP = ipAddresses[0];
        return "ws://" + localIP.ToString() + ":" + port;
    }

    public static IPAddress GetMyIpAddress()
    {
        string hostName = Dns.GetHostName();
        IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
        return ipAddresses[0];
    }
}
