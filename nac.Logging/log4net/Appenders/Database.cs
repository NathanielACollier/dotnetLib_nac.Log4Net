using System;

namespace nac.Logging.log4netLib.Appenders;

public static class Database
{
    
    
    public static void AddADONetDatabaseAppender(Type connectionType, string connectionString, string logTableName, string applicationName)
    {
        var repo = log4netLib.Setup.GetRepo();

        // here is an example of configuring this in code: https://codesofair.wordpress.com/2012/02/01/creating-log4net-appenders-programatically-in-c/

        var dbAppender = new log4net.Appender.AdoNetAppender();
        dbAppender.BufferSize = 1; // very important or your logs will be delayed

        dbAppender.ConnectionType = connectionType.AssemblyQualifiedName;
        dbAppender.ConnectionString = connectionString;
        dbAppender.CommandType = System.Data.CommandType.Text;

        var sessionID = Guid.NewGuid().ToString("N"); // one value per program run so we can group by that to see all the entries for a single session

        dbAppender.CommandText = $@"
            INSERT INTO {logTableName}([Date],[Application],[Thread],[Level],[Logger],[Message],[Exception], [Session])
            VALUES (@log_date,'{applicationName}', @thread, @log_level, @logger, @message, @exception, '{sessionID}')
        ";

        // There is a problem with Log4Net 2.0.4 so use 2.0.5 or greater for ADONetDatabaseAdapter
        //   see: https://stackoverflow.com/questions/33696604/log4net-adonetappender-sqlparametercollection-does-not-contain-parameters

        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@log_date",
            Layout = new log4net.Layout.RawTimeStampLayout(),
            DbType = System.Data.DbType.DateTime
        });


        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@thread",
            DbType = System.Data.DbType.String,
            Size = 255,
            Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%thread"))
        });

        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@log_level",
            DbType = System.Data.DbType.String,
            Size = 50,
            Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%level"))
        });

        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@logger",
            DbType = System.Data.DbType.String,
            Size = 255,
            Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%logger"))
        });


        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@message",
            DbType = System.Data.DbType.String,
            Size = 4000,
            Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.PatternLayout("%message"))
        });

        dbAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter
        {
            ParameterName = "@exception",
            DbType = System.Data.DbType.String,
            Size = 2000,
            Layout = new log4net.Layout.Layout2RawLayoutAdapter(new log4net.Layout.ExceptionLayout())
        });


        dbAppender.ActivateOptions();
        repo.Root.AddAppender(dbAppender);
    }
    
    
}