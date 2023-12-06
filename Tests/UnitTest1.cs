using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class UnitTest1
{
    private static nac.Log4Net.Logger log = new();
    
    [ClassInitialize]
    public static void Setup(TestContext context)
    {
        nac.Log4Net.log4netLib.Appenders.Console.AddDebugConsoleAppender();
    }
    
    [TestMethod]
    public void TestMethod1()
    {
        log.Info("Hello World!");
        log.Warn("This is a warning message");
        log.Error("Here is an Error");
        log.Fatal("And this is a fatal example");
    }
}