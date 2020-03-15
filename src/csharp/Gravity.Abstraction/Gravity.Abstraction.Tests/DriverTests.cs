using Gravity.Abstraction.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                "    'driverBinaries':'" + TestContext.Properties["gridEndpoint"] + "'" +
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