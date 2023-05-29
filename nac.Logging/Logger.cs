using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

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

    public void Debug(string messageText, [CallerMemberName] string callerMemberName = "")
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            MessageText = messageText,
            Level = Models.LogLevel.Debug
        });
    }

    public void Info(string messageText, [CallerMemberName] string callerMemberName = "")
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            MessageText = messageText,
            Level = Models.LogLevel.Info
        });
    }

    public void Warn(string messageText, [CallerMemberName] string callerMemberName = "")
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            MessageText = messageText,
            Level = Models.LogLevel.Warn
        });
    }


    public void Error(string messageText, [CallerMemberName] string callerMemberName = "")
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
            MessageText = messageText,
            Level = Models.LogLevel.Error
        });
    }

    public void Fatal(string messageText, [CallerMemberName] string callerMemberName = "")
    {
        CreateLogEntry(new Models.LogEntryCreationInfo
        {
            Source = this.source,
            CallingMemberName = callerMemberName,
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
        var logEntry = new Models.LogEntry
        {
            Source = creationInfo.Source,
            CallingMemberName = creationInfo.CallingMemberName,
            Occured = DateTime.Now,
            Level = creationInfo.Level,
            MessageText = creationInfo.MessageText
        };

        var appendersAtCallTime = new List<Appenders.LogAppender>(appenders); // make a copy to prevent collection was modified in foreach loop errors
        foreach (var _a in appendersAtCallTime)
        {
            _a.HandleLog(logEntry);
        }
    }
}

