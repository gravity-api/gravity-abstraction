using Gravity.Abstraction.WebDriver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            // execute
            var actual = DoCreateRemote(
                onDriver: "ChromeDriver",
                onTest: MethodBase.GetCurrentMethod().Name);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteFirefox()
        {
            // execute
            var actual = DoCreateRemote(
                onDriver: "FirefoxDriver",
                onTest: MethodBase.GetCurrentMethod().Name);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteInternetExplorer()
        {
            // execute
            var actual = DoCreateRemote(
                onDriver: "IEDriverServer",
                onTest: MethodBase.GetCurrentMethod().Name);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteEdge()
        {
            // execute
            var actual = DoCreateRemote(
                onDriver: "MicrosoftWebDriver",
                onTest: MethodBase.GetCurrentMethod().Name);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        private bool DoCreateRemote(string onDriver, string onTest)
        {
            // setup
            var driverParams = "" +
                "{" +
                "    'driver':'RemoteWebDriver'," +
                "    'remoteDriver':'" + onDriver + "'," +
                "    'driverBinaries':'" + TestContext.Properties["Grid.Endpoint"] + "'," +
                "    'capabilities': {" +
                "        'project':'" + TestContext.Properties["Project.Name"] + "'," +
                "        'build':'" + TestContext.Properties["Build.Number"] + "'," +
                "        'name':'" + onTest + "'" +
                "    }" +
                "}";

            // execute
            var driver = new DriverFactory(driverParams).Create();

            // assertion
            var actual = driver != null;

            // cleanup
            driver.Dispose();

            // result
            return actual;
        }
    }
}