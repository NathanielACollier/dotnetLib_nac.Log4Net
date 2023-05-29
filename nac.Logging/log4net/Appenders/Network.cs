using System.Linq;

namespace nac.Logging.log4netLib.Appenders;

public class Network
{
    private static System.Net.IPAddress GetInterNetworkIPv4AddressFromHostName( string hostname )
    {
        return System.Net.Dns.GetHostEntry(hostname)
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
    }

    public static void AddUdpAppenderWithLog4JFormat( int remotePort)
    {
        var localIPAddress = GetInterNetworkIPv4AddressFromHostName(System.Net.Dns.GetHostName());

        AddUdpAppenderWithLog4JFormat(localIPAddress, remotePort);
    }


    /// <summary>
    /// This does a lookup on the address you enter.  Right now exceptions are not caught...
    /// </summary>
    /// <param name="remoteAddress"></param>
    /// <param name="remotePort"></param>
    public static void AddUdpAppenderWithLog4JFormat(string remoteAddress, int remotePort)
    {
        var ipAddress = GetInterNetworkIPv4AddressFromHostName(remoteAddress);
        // GetHostAddresses is going to throw an exception if you enter a bad address.  We need to decide if we want to forward that exception or catch it...
        AddUdpAppenderWithLog4JFormat(ipAddress, remotePort);
    }

    public static void AddUdpAppenderWithLog4JFormat(System.Net.IPAddress remoteAddress, int remotePort)
    {
        var repo = log4netLib.Setup.GetRepo();

        var layout = new log4net.Layout.XmlLayoutSchemaLog4j(true); // locationInformation is the true/false parameter

        // log pattern and all that type stuff are for files, with UDP we are sending xml info
        var udpAppender = new log4net.Appender.UdpAppender();
        udpAppender.Layout = layout;
        udpAppender.RemoteAddress = remoteAddress;
        udpAppender.RemotePort = remotePort;

        udpAppender.ActivateOptions();
        repo.Root.AddAppender(udpAppender);
    }
}