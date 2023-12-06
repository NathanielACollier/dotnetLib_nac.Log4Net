using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace nac.Log4Net;

public class Logger : nac.Logging.Logger
{

    public Logger() : base()
    {
        OnNewMessage += (_s, _message) =>
        {
            CreateLogEntry(new Models.LogEntryCreationInfo
            {
                Source = new Models.LoggerSourceInfo(_message.CallingClassType),
                CallingMemberName = _message.CallingMemberName,
                CallingFilePath = _message.CallingSourceFilePath,
                CallingLineNumber = _message.CallingSourceLineNumber,
                MessageText = _message.Message,
                Level = getLogLevelFromText(_message.Level)
            });
        };
    }



    public static Models.LogLevel getLogLevelFromText(string logLevelText)
    {
        return logLevelText.Trim().ToLower() switch
        {
            "info" => Models.LogLevel.Info,
            "debug" => Models.LogLevel.Debug,
            "warn" => Models.LogLevel.Warn,
            "error" => Models.LogLevel.Error,
            "fatal" => Models.LogLevel.Fatal,
            _ => throw new Exception($"Unknown log level [{logLevelText}]")
        };
    }


    public static void CreateLogEntry(Models.LogEntryCreationInfo creationInfo)
    {
        var apacheLog = log4net.LogManager.GetLogger(name: creationInfo.Source.ClassName);

        var logEvent = new log4net.Core.LoggingEvent(new LoggingEventData
        {
            Level = log4netLib.Appenders.Util.GetLogLevel(creationInfo.Level),
            Message = creationInfo.MessageText,
            TimeStampUtc = DateTime.Now.ToUniversalTime(),
            LocationInfo = new log4net.Core.LocationInfo(className: creationInfo.Source.ClassName,
                methodName: creationInfo.CallingMemberName,
                fileName: creationInfo.CallingFilePath,
                lineNumber: creationInfo.CallingLineNumber.ToString()
                ),
            LoggerName = $"{creationInfo.Source.ClassName}.{creationInfo.CallingMemberName}"
        } );
        
        apacheLog.Logger.Log(logEvent);
    }
}

