using Gravity.Abstraction.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Gravity.Abstraction.Tests
{
    [TestClass]
    public class DriverTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CreateRemoteChrome()
        {
            // setup
            var driverParams = "" +
                "{" +
                "    'driver':'RemoteWebDriver'," +
                "    'remoteDriver':'ChromeDriver'," +
                "    'driverBinaries':'" + TestContext.Properties["Grid.Endpoint"] + "'," +
                "    'capabilities': {" +
                "        'project': '" + TestContext.Properties["Project.Name"] + "'," +
                "        'build': '" + TestContext.Properties["Build.Number"] + "'," +
                "        'name': '" + MethodBase.GetCurrentMethod().Name + "'" +
                "    }" +
                "}";

            // execute
            var driver = new DriverFactory(driverParams).Create();

            // assertion
            Assert.IsTrue(driver != null);

            // cleanup
            driver.Dispose();
        }
    }
}