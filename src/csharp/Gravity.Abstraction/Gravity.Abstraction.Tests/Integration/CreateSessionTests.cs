﻿using Gravity.Abstraction.Contracts;
using Gravity.Abstraction.Tests.Base;
using Gravity.Abstraction.WebDriver;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using OpenQA.Selenium.Remote;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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
    }
}