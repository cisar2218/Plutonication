using System.Net.Sockets;
using System.Net;

namespace Plutonication;
public class ConnectionManager
{
    public static void TmpConnectMethod()
    {
        int port = 8080;
        string serverAddr = "127.0.0.1";
        TcpClient client = new TcpClient(serverAddr, port);
        NetworkStream stream = client.GetStream();

        Byte[] data = new Byte[256];
        String responseData = String.Empty;

        Int32 bytes = stream.Read(data, 0, data.Length);
        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);
    }
    public static void TmpListenMethod()
    {
        TcpListener server = null;
        try
        {
            server = ConnectionManager.ListenLocal();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return;
        }
        server.Start();
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            // send private key
            string data = "somePrivateKey";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            stream.Write(msg, 0, msg.Length);

            Console.WriteLine("Sent: {0}", data);

            client.Close();
        }
    }
    public static TcpClient Connect(string serverAddr, int port)
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
            throw;
        }
    }

    public static TcpListener ListenLocal()
    {
        const int port = 8080;
        return Listen(GetMyIpAddress(), port);
    }
    public static TcpListener Listen(string ipLocal, int port)
    {
        return Listen(IPAddress.Parse(ipLocal), port);
    }
    public static TcpListener Listen(IPAddress ipLocal, int port)
    {
        try
        {
            TcpListener listener = new TcpListener(ipLocal, port);
            return listener;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public static string GetMyWebSocketLink(int port)
    {
        string hostName = Dns.GetHostName();
        IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
        IPAddress localIP = ipAddresses[0];
        return "ws://" + localIP.ToString() + ":" + port;
    }

    public static string GetMyIpAddress()
    {
        string hostName = Dns.GetHostName();
        IPAddress[] ipAddresses = Dns.GetHostEntry(hostName).AddressList;
        var ads = hostName + "\n";
        foreach (var address in ipAddresses)
        {
            ads += address.ToString() + "\n";
        }
        return ads;
        var ip = ipAddresses.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                 .FirstOrDefault();
        return ip.ToString() ?? "";
    }
}
