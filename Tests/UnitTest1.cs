using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class UnitTest1
{
    private static nac.Logging.Logger log = new();
    
    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        nac.Logging.log4netLib.Appenders.Console.AddDebugConsoleAppender();
    }
    
    [TestMethod]
    public void TestMethod1()
    {
        log.Info("Hello World!");
    }
}