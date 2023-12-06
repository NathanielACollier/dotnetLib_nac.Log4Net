namespace nac.Log4Net.log4netLib.Appenders;

public static class SMTP
{
    public static void AddSmtpAppender(string toAddress, string fromAddress, string subject, string smtpHost, 
        int smtpPort = 25, 
        int bufferSize = 100000,
        log4net.Core.Level threshold = null,
        string logPattern = Util.DefaultLogPattern)
    {
        // it's cast to a Hierarchy because we need repo.Root to add appenders
        var repo = log4netLib.Setup.GetRepo();

        var logPatternLayout = new log4net.Layout.PatternLayout(logPattern);

        var smtpAppender = new log4net.Appender.SmtpAppender();

        smtpAppender.Layout = logPatternLayout;

        smtpAppender.To = toAddress;
        smtpAppender.From = fromAddress;
        smtpAppender.Subject = subject;
        smtpAppender.SmtpHost = smtpHost;
        smtpAppender.Port = smtpPort;
        smtpAppender.BufferSize = bufferSize;

        if( threshold != null )
        {
            smtpAppender.Threshold = threshold;
        }

        smtpAppender.ActivateOptions();
        repo.Root.AddAppender(smtpAppender);
    }
    
    
    
}