using System.Text;

namespace nac.Logging.log4netLib;

public static class Setup
{
    private static bool isLog4netInitialized = false;

    private static void InitializeLog4Net()
    {
        if (isLog4netInitialized)
        {
            return;
        }

        isLog4netInitialized = true;
	    
        string configText = @"
						<log4net>
							<root>
							<level value='ALL' />   
							</root>
						</log4net>
						";
        
        log4net.LogManager.ResetConfiguration();
        // configure from stream
        System.IO.Stream configStream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(configText));
        log4net.Config.XmlConfigurator.Configure(configStream);

    }




}