using OpenQA.Selenium.Mock;
using Gravity.Services.DataContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Gravity.Abstraction.Contracts;
using Gravity.Abstraction.Interfaces;
using Gravity.Abstraction.Attributes;
using Gravity.Abstraction.Extensions;
using System.Text.RegularExpressions;

namespace Gravity.Abstraction.WebDriver
{
    public class DriverFactory
    {
        // constants
        private const BindingFlags BINDING = BindingFlags.Instance | BindingFlags.NonPublic;

        private const string FTL2 = "initialize method for [driver={0}; remote={1}] driver was not found. " +
            "please make sure that you have provided a correct driver-parameters";

        // members: state
        private readonly string driverParams;
        private readonly JToken driverToken;
        private readonly JToken driverBinariesToken;
        private readonly JToken optionsToken;
        private readonly JToken serviceToken;
        private readonly Dictionary<string, object> capabilities;

        /// <summary>
        /// creates an instance of driver-factory, parsing driver-parameters to create
        /// web-driver instance
        /// </summary>
        /// <param name="driverParams">driver-parameters to create web-driver by</param>
        public DriverFactory(string driverParams)
        {
            this.driverParams = driverParams;

            // publish tokens
            var paramsToken = JToken.Parse(driverParams);
            driverToken = paramsToken.ByName("driver")?.FirstOrDefault()?.First;
            driverBinariesToken = paramsToken.ByName("driverBinaries")?.FirstOrDefault()?.First;
            optionsToken = paramsToken.ByName("options")?.FirstOrDefault()?.First;
            serviceToken = paramsToken.ByName("service")?.FirstOrDefault()?.First;

            // populate capabilities
            var capabilitiesToken = paramsToken.ByName("capabilities")?.FirstOrDefault()?.First;
            capabilities = capabilitiesToken == null
                ? new Dictionary<string, object>()
                : JsonConvert.DeserializeObject<Dictionary<string, object>>($"{capabilitiesToken}");
        }

        #region *** create driver factory                ***
        /// <summary>
        /// generate web-driver instance based on driver-parameters
        /// </summary>
        /// <returns>web-driver interface generate from the corresponding driver instance</returns>
        public IWebDriver Create()
        {
            // get driver information
            var driver = $"{driverToken}";
            var driverBinaries = $"{driverBinariesToken}";

            // setup conditions
            var isRemote = Regex.IsMatch(input: driverBinaries, pattern: "^(http(s)?)://");

            // get method
            var method = GetMethod(driver, isRemote);

            // generate driver
            return (IWebDriver)method.Invoke(this, new object[] { driverBinaries });
        }

        // parse driver-parameters and driver-options for remote web-driver
        private DriverOptions GetOptions<TOptions, TParams>(string platformName)
            where TOptions : DriverOptions, new()
            where TParams : DriverOptionsParams, IOptionable<TOptions>
        {
            // parse token
            var isOptions = optionsToken?.HasValues == true;

            // null validation
            if (!isOptions)
            {
                return new TOptions
                {
                    PlatformName = platformName
                };
            }

            // deserialize options
            var paramsObj = JsonConvert.DeserializeObject<TParams>($"{optionsToken}");

            // return dynamic object
            var options = paramsObj.ToDriverOptions();
            if (!string.IsNullOrEmpty(platformName))
            {
                options.PlatformName = platformName;
            }
            return options;
        }

        // parse driver-parameters and server-options for remote web-driver
        private DriverService GetService<TService, TParams>(string driverBinaries)
            where TService : DriverService
            where TParams : DriverServiceParams, IServiceable<TService>
        {
            // null validation
            if (serviceToken?.Any() != true)
            {
                return default;
            }

            // deserialize options
            var paramsObj = JsonConvert.DeserializeObject<TParams>(driverParams);
            paramsObj.HostName = string.IsNullOrEmpty(paramsObj.HostName) ? Environment.MachineName : paramsObj.HostName;

            // return dynamic object
            return paramsObj.ToDriverService(driverBinaries);
        }

        // get capabilities interface and manipulate capabilities
        private ICapabilities GetCapabilities(DriverOptions driverOptions, Dictionary<string, object> rawCapabilities)
        {
            // convert options
            var options = driverOptions.ToCapabilities();

            // get capabilities field
            var isFieldNull = options
                .GetType()
                .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance) == null;

            Dictionary<string, object> cap;
            if (isFieldNull)
            {
                cap = (Dictionary<string, object>)options
                    .GetType()
                    .BaseType
                    .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(options);
            }
            else
            {
                cap = (Dictionary<string, object>)options
                    .GetType()
                    .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(options);
            }

            // exit conditions
            if (cap == null || rawCapabilities == null)
            {
                return options;
            }

            // add capabilities
            foreach (var item in rawCapabilities)
            {
                cap[item.Key] = item.Value;
            }
            return options;
        }

        // get web-driver method
        private MethodInfo GetMethod(string driver, bool isRemote)
        {
            // collect all methods
            var methods = GetType().GetMethods(BINDING).Where(m => m.IsDriverMethod());

            // get method
            var method = methods.FirstOrDefault(m => m.DriverMatch(driver, isRemote));

            // exit conditions
            if (method == null)
            {
                var message = string.Format(FTL2, driver, isRemote);
                Trace.TraceError(message);
                throw new MethodAccessException(message);
            }

            // return method to invoke
            return method;
        }
        #endregion

#pragma warning disable IDE0051
        #region *** driver generating methods repository ***
        // LOCAL WEB DRIVERS
        [DriverMethod(Driver = Driver.Chrome)]
        private IWebDriver GetChrome(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<ChromeOptions, ChromeOptionsParams>(null);
            var service = GetService<ChromeDriverService, ChromeServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<ChromeDriver>(options, service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Firefox)]
        private IWebDriver GetFirefox(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<FirefoxOptions, FirefoxOptionsParams>(null);
            var service = GetService<FirefoxDriverService, FirefoxServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<FirefoxDriver>(options, service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.InternetExplorer)]
        private IWebDriver GetInternetExplorer(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<InternetExplorerOptions, InternetExplorerOptionsParams>(null);
            var service = GetService<InternetExplorerDriverService, InternetExplorerServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<InternetExplorerDriver>(options, service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Edge)]
        private IWebDriver GetEdge(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<EdgeOptions, EdgeOptionsParams>(null);
            var service = GetService<EdgeDriverService, EdgeServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<EdgeDriver>(options, service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Mock)]
        private IWebDriver GetMock(string driverBinaries)
        {
            return new MockWebDriver(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Safari)]
        private IWebDriver GetSafari(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<SafariOptions, SafariOptionsParams>(null);
            var service = GetService<SafariDriverService, SafariServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<SafariDriver>(options, service, driverBinaries);
        }

        // REMOTE WEB DRIVERS (Appium included)
        [DriverMethod(Driver = Driver.Chrome, RemoteDriver = true)]
        private IWebDriver GetRemoteChrome(string driverBinaries)
        {
            return GetRemote<ChromeOptions, ChromeOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Firefox, RemoteDriver = true)]
        private IWebDriver GetRemoteFirefox(string driverBinaries)
        {
            return GetRemote<FirefoxOptions, FirefoxOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.InternetExplorer, RemoteDriver = true)]
        private IWebDriver GetRemoteInternetExplorer(string driverBinaries)
        {
            return GetRemote<InternetExplorerOptions, InternetExplorerOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Edge, RemoteDriver = true)]
        private IWebDriver GetRemoteEdge(string driverBinaries)
        {
            return GetRemote<EdgeOptions, EdgeOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Android, RemoteDriver = true)]
        private IWebDriver GetRemoteAndroid(string driverBinaries)
        {
            return GetMobile<AppiumOptions, AppiumOptionsParams, AndroidDriver<IWebElement>>(driverBinaries, "Android");
        }

        [DriverMethod(Driver = Driver.iOS, RemoteDriver = true)]
        private IWebDriver GetRemoteIos(string driverBinaries)
        {
            return GetMobile<AppiumOptions, AppiumOptionsParams, IOSDriver<IWebElement>>(driverBinaries, "iOS");
        }

        [DriverMethod(Driver = Driver.Safari, RemoteDriver = true)]
        private IWebDriver GetRemoteSafari(string driverBinaries)
        {
            return GetRemote<SafariOptions, SafariOptionsParams>(driverBinaries);
        }
        #endregion
#pragma warning restore

        // TODO: add default constructor options when there is no binaries to load
        // Gets local web driver instance
        private IWebDriver GetLocal<TDriver>(DriverOptions options, DriverService service, string driverBinaries) where TDriver : IWebDriver
        {
            // get driver type
            var driverType = typeof(TDriver);

            // setup conditions
            var isOpions = options != null;
            var isService = service != null;

            // setup driver binaries
            driverBinaries = driverBinaries.Equals(".") ? Environment.CurrentDirectory : driverBinaries;

            // factor
            if (isOpions && isService)
            {
                return (IWebDriver)Activator.CreateInstance(driverType, new object[] { service, options });
            }
            if (isOpions)
            {
                return (IWebDriver)Activator.CreateInstance(driverType, new object[] { driverBinaries, options });
            }
            if (isService)
            {
                return (IWebDriver)Activator.CreateInstance(driverType, new object[] { service });
            }

            // default constructor with binaries only
            return (IWebDriver)Activator.CreateInstance(driverType, new object[] { driverBinaries });
        }

        // Gets remote web driver instance
        private IWebDriver GetRemote<TOptions, TParams>(string driverBinaries)
            where TOptions : DriverOptions, new()
            where TParams : DriverOptionsParams, IOptionable<TOptions>
        {
            // get constructor arguments
            var options = GetOptions<TOptions, TParams>(null);
            var cap = GetCapabilities(options, capabilities);

            // factor web driver
            return new RemoteWebDriver(new Uri(driverBinaries), cap);
        }

        // Gets remote mobile web driver instance
        private IWebDriver GetMobile<TOptions, TParams, TDriver>(string driverBinaries, string platformName)
            where TOptions : AppiumOptions, new()
            where TParams : DriverOptionsParams, IOptionable<TOptions>
            where TDriver : AppiumDriver<IWebElement>
        {
            // get constructor arguments
            var options = GetOptions<TOptions, TParams>(platformName);
            GetCapabilities(options, capabilities);

            // factor web driver
            return (IWebDriver)Activator.CreateInstance(typeof(TDriver), new object[] { new Uri(driverBinaries), options });
        }
    }
}