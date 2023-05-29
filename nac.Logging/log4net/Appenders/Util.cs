using nac.Logging.Models;

namespace nac.Logging.log4netLib.Appenders;

public static class Util
{
    public const string DefaultLogPattern = "[%date{yyyy-MM-dd hh:mm:sstt}] %-5level %logger - %message%newline";


    public static log4net.Core.Level GetLogLevel(nac.Logging.Models.LogLevel nacLevel)
    {
        return nacLevel switch
        {
            LogLevel.Debug => log4net.Core.Level.Debug,
            LogLevel.Info => log4net.Core.Level.Info,
            LogLevel.Warn => log4net.Core.Level.Warn,
            LogLevel.Error => log4net.Core.Level.Error,
            LogLevel.Fatal => log4net.Core.Level.Fatal,
            _ => log4net.Core.Level.Info
        };
    }
    
    
}