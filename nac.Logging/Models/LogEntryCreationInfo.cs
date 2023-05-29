using System;
using System.Collections.Generic;
using System.Text;

namespace nac.Logging.Models;

public class LogEntryCreationInfo
{
    public string MessageText { get; set; }
    public string CallingMemberName { get; set; }

    public LogLevel Level { get; set; }

    public Models.LoggerSourceInfo Source { get; set; }
    public int CallingLineNumber { get; set; }
    public string CallingFilePath { get; set; }
}
