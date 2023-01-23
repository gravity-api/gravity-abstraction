using Gravity.Abstraction.Contracts;
using Gravity.Abstraction.Tests.Base;
using Gravity.Abstraction.WebDriver;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Safari;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace Gravity.Abstraction.Tests.Integration
{
    [TestClass]
    public class CreateSessionTests : TestSuite
    {
        public TestContext TestContext { get; set; }

        [TestCleanup]
        public void Cleanup()
        {
            UpdateBrowserStack(onContext: TestContext);
        }

        [TestMethod]
        public void CreateRemoteChrome()
        {
            // execute
            var actual = CreateRemoteDriver(
                onDriver: "ChromeDriver",
                onTest: MethodBase.GetCurrentMethod().Name,
                onContext: TestContext);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteFirefox()
        {
            // execute
            var actual = CreateRemoteDriver(
                onDriver: "FirefoxDriver",
                onTest: MethodBase.GetCurrentMethod().Name,
                onContext: TestContext);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteInternetExplorer()
        {
            // execute
            var actual = CreateRemoteDriver(
                onDriver: "IEDriverServer",
                onTest: MethodBase.GetCurrentMethod().Name,
                onContext: TestContext);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        [TestMethod]
        public void CreateRemoteEdge()
        {
            // execute
            var actual = CreateRemoteDriver(
                onDriver: "MicrosoftWebDriver",
                onTest: MethodBase.GetCurrentMethod().Name,
                onContext: TestContext);

            // assertion
            Assert.IsTrue(condition: actual);
        }

        //[TestMethod]
        public void CreateRemoteUia()
        {
            // execute
            var actual = CreateRemoteDriver(
                onDriver: "UiaDriver",
                onTest: MethodBase.GetCurrentMethod().Name,
                onContext: TestContext);

            // assertion
            //Assert.IsTrue(condition: actual);
        }

        //[TestMethod]
        public void CreateRemoteSafariMobile()
        {
            // execute
            //var driverParams = new
            //{
            //    DriverBinaries = "https://gravityapi1:pyhBifB6z1YxJv53xLip@hub-cloud.browserstack.com/wd/hub",
            //    Driver = "RemoteWebDriver",
            //    Capabilities = new Dictionary<string, object>
            //    {
            //        ["bstack:options"] = new Dictionary<string, object>
            //        {
            //            ["os"] = "Windows",
            //            ["osVersion"] = "10",
            //            ["browserName"] = "chrome",
            //            ["local"] = "false",
            //            ["userName"] = "gravityapi1",
            //            ["accessKey"] = "pyhBifB6z1YxJv53xLip"
            //        }
            //    }
            //};
            var driverParams = new
            {
                DriverBinaries = "https://gravityapi1:pyhBifB6z1YxJv53xLip@hub-cloud.browserstack.com/wd/hub",
                Driver = "RemoteWebDriver",
                Capabilities = new Dictionary<string, object>
                {
                    ["bstack:options"] = new Dictionary<string, object>
                    {
                        ["osVersion"] = "10.0",
                        ["deviceName"] = "Samsung Galaxy S20",
                        ["local"] = "false",
                        ["userName"] = "gravityapi1",
                        ["accessKey"] = "pyhBifB6z1YxJv53xLip",
                        ["browserName"] = "chrome"
                    }
                }
            };

            var factory = new DriverFactory(JsonSerializer.Serialize(driverParams, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));

            var driver = factory.Create();
            driver.Url = "http://tbo.jtpstage.com/en-ca/tests/qa-tests-irina/aptitude_test_automation?authToken=nB10V@$Z87du$p@!Rz";
            driver.Dispose();
        }
    }
}
