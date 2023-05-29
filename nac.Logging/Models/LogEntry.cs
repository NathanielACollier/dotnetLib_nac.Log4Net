using System;
using System.Collections.Generic;
using System.Text;

namespace nac.Logging.Models;

public class LogEntry
{
    public string MessageText { get; set; }
    public LogLevel Level { get; set; }
    public DateTime Occured { get; set; }
    public Models.LoggerSourceInfo Source { get; set; }
    public string CallingMemberName { get; set; }


    public override string ToString()
    {
        string prefix = Models.LogLevelUtility.LevelToString(this.Level);

        string line = $"[{this.Occured:yyyy-MM-dd hh.mm.sstt}] {prefix} {this.Source.ClassName}.{this.CallingMemberName} - {this.MessageText}";
        return line;
    }
}
