namespace nac.Logging.log4netLib.Appenders;

public static class Notify
{
    public static void AddNotifyAppender(log4netLib.Appenders.models.NotifyAppender.NewLogEntryHandler newLogEntryAction,
        log4net.Core.Level threshold = null,
        string logPattern = Util.DefaultLogPattern)
    {
        // add in our notify appender
        var repository = log4netLib.Setup.GetRepo();

        var logPatternLayout = new log4net.Layout.PatternLayout(logPattern);

        if (newLogEntryAction != null)
        {
            var notifyAppender = new models.NotifyAppender();
            notifyAppender.NewLogEntry += newLogEntryAction;

            if (threshold != null)
            {
                notifyAppender.Threshold = threshold;
            }

            notifyAppender.Layout = logPatternLayout;
            notifyAppender.ActivateOptions();

            repository.Root.AddAppender(notifyAppender);
        }
    }
    
    
    
    
}