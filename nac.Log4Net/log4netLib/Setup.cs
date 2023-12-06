using System.Text;
using log4net.Repository.Hierarchy;

namespace nac.Log4Net.log4netLib;

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

    public static Hierarchy GetRepo()
    {
	    InitializeLog4Net();

	    var repo = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
	    return repo;
    }

    
    public static bool IsLog4NetConfigured()
    {
	    // source of this type of check is: http://neilkilbride.blogspot.com/2008/04/configure-log4net-only-once.html
	    var repo = log4net.LogManager.GetRepository();

	    return repo.Configured;
    }
    

}