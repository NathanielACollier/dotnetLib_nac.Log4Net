using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Core;

namespace nac.Logging;

public class Logger
{

    private Models.LoggerSourceInfo source;

    public Logger()
    {
        // get calling assembly
        var mth = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
        var _class_to_log = mth.ReflectedType;

        this.source = new Models.LoggerSourceInfo(_class_to_log);
    }

    /**
     * <summary>
     *  This constructor should only be used for compatibility with other logging frameworks
     * </summary>
     */
    public Logger(Models.LoggerSourceInfo __source)
    {
        this.source = __source;
    }

    public void Debug(string messageText, 
        [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            CallingFilePath = sourceFilePath,
            CallingLineNumber = sourceLineNumber,
            MessageText = messageText,
            Level = Models.LogLevel.Debug
        });
    }

    public void Info(string messageText, [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            CallingFilePath = sourceFilePath,
            CallingLineNumber = sourceLineNumber,
            MessageText = messageText,
            Level = Models.LogLevel.Info
        });
    }

    public void Warn(string messageText, [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            CallingFilePath = sourceFilePath,
            CallingLineNumber = sourceLineNumber,
            MessageText = messageText,
            Level = Models.LogLevel.Warn
        });
    }


    public void Error(string messageText, [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            CallingFilePath = sourceFilePath,
            CallingLineNumber = sourceLineNumber,
            MessageText = messageText,
            Level = Models.LogLevel.Error
        });
    }

    public void Fatal(string messageText, [CallerMemberName] string callerMemberName = "",
        [CallerFilePath] string sourceFilePath = "", 
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            CallingFilePath = sourceFilePath,
            CallingLineNumber = sourceLineNumber,
            MessageText = messageText,
            Level = Models.LogLevel.Fatal
        });
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
                )
        } );
        
        apacheLog.Logger.Log(logEvent);
    }
}

