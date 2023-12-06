using System;
using System.Collections.Generic;
using System.Text;

namespace nac.Log4Net.Models;

public class LogEntry
{
    public string MessageText { get; set; }
    public LogLevel Level { get; set; }
    public DateTime Occured { get; set; }
    public Models.LoggerSourceInfo Source { get; set; }
    public string CallingMemberName { get; set; }

}
