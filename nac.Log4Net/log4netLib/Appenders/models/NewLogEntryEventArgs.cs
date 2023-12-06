using System;

namespace nac.Log4Net.log4netLib.Appenders.models;

public class NewLogEntryEventArgs : EventArgs
{

    public string FormattedMessage { get; set; }

    public log4net.Core.LoggingEvent SourceEvent { get; set; }

    public string Level { get { return SourceEvent.Level.DisplayName; } }

    public string Message { get { return SourceEvent.RenderedMessage;  } }
    public string Domain { get { return SourceEvent.Domain; } }
    public string LoggerName { get { return SourceEvent.LoggerName; } }
    public string ThreadName { get { return SourceEvent.ThreadName; } }
    public string UserName { get { return SourceEvent.UserName; } }
    public DateTime TimeStamp { get { return SourceEvent.TimeStamp;  } }
}