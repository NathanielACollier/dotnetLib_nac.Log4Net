namespace nac.Log4Net.log4netLib.Appenders;

public static class Console
{
    
    public static void AddDebugConsoleAppender(log4net.Core.Level threshold = null,
        string logPattern = Util.DefaultLogPattern)
    {
        var repo = log4netLib.Setup.GetRepo();

        var debugAppender = new log4net.Appender.DebugAppender
        {
            Threshold = threshold,
            Layout = new log4net.Layout.PatternLayout(logPattern)
        };

        debugAppender.ActivateOptions();
        repo.Root.AddAppender(debugAppender);
    }
    
    
    /// <summary>
    /// See example at this place: http://aaubry.net/configuring-log4net-coloredconsoleappender-in-code.html
    /// </summary>
    /// <param name="threshold"></param>
    /// <param name="logPattern"></param>
    public static void AddColoredConsoleAppender( log4net.Core.Level threshold = null,
                                                    string logPattern = Util.DefaultLogPattern)
        {
            var repo = log4netLib.Setup.GetRepo();

        var coloredConsoleAppender = new log4net.Appender.ColoredConsoleAppender
        {
            Threshold = (threshold == null) ? log4net.Core.Level.All : threshold,
            Layout = new log4net.Layout.PatternLayout(logPattern)
        };

        coloredConsoleAppender.AddMapping(new log4net.Appender.ColoredConsoleAppender.LevelColors
        {
            Level = log4net.Core.Level.Info,
            ForeColor = log4net.Appender.ColoredConsoleAppender.Colors.White | log4net.Appender.ColoredConsoleAppender.Colors.HighIntensity
        });

        coloredConsoleAppender.AddMapping(new log4net.Appender.ColoredConsoleAppender.LevelColors
        {
            Level = log4net.Core.Level.Debug,
            ForeColor = log4net.Appender.ColoredConsoleAppender.Colors.White | log4net.Appender.ColoredConsoleAppender.Colors.HighIntensity,
            BackColor = log4net.Appender.ColoredConsoleAppender.Colors.Blue
        });

        coloredConsoleAppender.AddMapping(new log4net.Appender.ColoredConsoleAppender.LevelColors
        {
            Level = log4net.Core.Level.Warn,
            ForeColor = log4net.Appender.ColoredConsoleAppender.Colors.Yellow | log4net.Appender.ColoredConsoleAppender.Colors.HighIntensity,
            BackColor = log4net.Appender.ColoredConsoleAppender.Colors.Purple
        });

        coloredConsoleAppender.AddMapping(new log4net.Appender.ColoredConsoleAppender.LevelColors
        {
            Level = log4net.Core.Level.Error,
            ForeColor = log4net.Appender.ColoredConsoleAppender.Colors.Yellow | log4net.Appender.ColoredConsoleAppender.Colors.HighIntensity,
            BackColor = log4net.Appender.ColoredConsoleAppender.Colors.Red
        });


        coloredConsoleAppender.ActivateOptions();
        repo.Root.AddAppender(coloredConsoleAppender);
    }
    
    
    
}