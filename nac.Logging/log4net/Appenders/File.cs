namespace nac.Logging.log4netLib.Appenders;

public static class File
{
    
    public static void AddRollingFileAppender(string filePath = "logs/log.txt",
        log4net.Core.Level threshold = null,
        string logPattern = Util.DefaultLogPattern,
        log4net.Appender.RollingFileAppender.RollingMode rollingMode = log4net.Appender.RollingFileAppender.RollingMode.Date,
        string rollingModeDatePattern = "yyyyMMdd",
        int maxNumberOfLogFilesToKeep = 5 )
    {
        // it's cast to a Hierarchy because we need repo.Root to add appenders
        var repo = log4netLib.Setup.GetRepo();

        var logPatternLayout = new log4net.Layout.PatternLayout(logPattern);

        var fileAppender = new log4net.Appender.RollingFileAppender();

        fileAppender.File = filePath;
        fileAppender.AppendToFile = true;
        fileAppender.Layout = logPatternLayout;
        fileAppender.MaxSizeRollBackups = maxNumberOfLogFilesToKeep; // http://stackoverflow.com/questions/95286/log4net-set-max-backup-files-on-rollingfileappender-with-rolling-date

        fileAppender.RollingStyle = rollingMode;

        if (rollingMode == log4net.Appender.RollingFileAppender.RollingMode.Date && !string.IsNullOrEmpty(rollingModeDatePattern))
        {
            fileAppender.DatePattern = "yyyyMMdd";
        }

        if (threshold != null)
        {
            fileAppender.Threshold = threshold;
        }

        fileAppender.ActivateOptions();
        repo.Root.AddAppender(fileAppender);
    }
}