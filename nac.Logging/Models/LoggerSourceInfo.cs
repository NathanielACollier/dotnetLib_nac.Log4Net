using System;
using System.Collections.Generic;
using System.Text;

namespace nac.Logging.Models;

public class LoggerSourceInfo
{
    public string ClassName { get; set; }
    public Type SourceType { get; set; }


    public LoggerSourceInfo()
    {
        // used by the Log4Net helper stuff to fake this info
    }

    public LoggerSourceInfo(Type typeInfo)
    {
        this.SourceType = typeInfo;
        this.ClassName = typeInfo.FullName;
    }
}
