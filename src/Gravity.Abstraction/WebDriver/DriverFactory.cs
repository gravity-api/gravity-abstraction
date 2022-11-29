using Gravity.Services.DataContracts;

using OpenQA.Selenium.Mock;

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
using System.Text.RegularExpressions;

using Gravity.Abstraction.Contracts;
using Gravity.Abstraction.Interfaces;
using Gravity.Abstraction.Attributes;
using Gravity.Abstraction.Extensions;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium.Uia;

namespace Gravity.Abstraction.WebDriver
{
    public class DriverFactory
    {
        // constants
        private const BindingFlags BINDING = BindingFlags.Instance | BindingFlags.NonPublic;
        private const string FTL2 = "Get-Driver -Driver {0} -Remote {1} = NotFound";

        // members: state
        private readonly IDictionary<string, object> driverParams;
        private readonly IDictionary<string, object> _options;
        private readonly IDictionary<string, object> service;
        private readonly IDictionary<string, object> _capabilities;
        private readonly int _commandTimeout;
        private readonly string driver;
        private readonly string driverBinaries;

        /// <summary>
        /// creates an instance of driver-factory, parsing driver-parameters to create
        /// web-driver instance
        /// </summary>
        /// <param name="driverParams">driver-parameters to create web-driver by</param>
        public DriverFactory(string driverParams)
            : this(JsonSerializer.Deserialize<IDictionary<string, object>>(driverParams))
        { }

        /// <summary>
        /// creates an instance of driver-factory, parsing driver-parameters to create
        /// web-driver instance
        /// </summary>
        /// <param name="driverParams">driver-parameters to create web-driver by</param>
        public DriverFactory(IDictionary<string, object> driverParams)
        {
            // setup
            this.driverParams = driverParams;

            // publish tokens
            _commandTimeout = driverParams.Find("commandTimeout", 600);
            driver = driverParams.Find("driver", Driver.Chrome);
            driverBinaries = driverParams.Find("driverBinaries", ".");
            _options = driverParams.Find("options", new Dictionary<string, object>());
            service = driverParams.Find("service", new Dictionary<string, object>());
            _capabilities = driverParams.Find("capabilities", new Dictionary<string, object>());
        }

        #region *** create driver factory ***
        /// <summary>
        /// generate web-driver instance based on driver-parameters
        /// </summary>
        /// <returns>web-driver interface generate from the corresponding driver instance</returns>
        public IWebDriver Create()
        {
            // setup conditions
            var isRemote = Regex.IsMatch(input: driverBinaries, pattern: "^(http(s)?)://");

            // get method
            var method = GetMethod(driver, isRemote);

            // generate driver
            return (IWebDriver)method.Invoke(this, new object[] { driverBinaries });
        }
        #endregion

        #region *** methods repository    ***
        // LOCAL WEB DRIVERS
        [DriverMethod(Driver = Driver.Chrome)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetChrome(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<ChromeOptions, ChromeOptionsParams>(null);
            var _service = GetService<ChromeDriverService, ChromeServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<ChromeDriver>(options, _service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Firefox)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetFirefox(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<FirefoxOptions, FirefoxOptionsParams>(null);
            var _service = GetService<FirefoxDriverService, FirefoxServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<FirefoxDriver>(options, _service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.InternetExplorer)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetInternetExplorer(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<InternetExplorerOptions, InternetExplorerOptionsParams>(null);
            var _service = GetService<InternetExplorerDriverService, InternetExplorerServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<InternetExplorerDriver>(options, _service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Edge)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetEdge(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<EdgeOptions, EdgeOptionsParams>(null);
            var _service = GetService<EdgeDriverService, EdgeServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<EdgeDriver>(options, _service, driverBinaries);
        }

        [DriverMethod(Driver = Driver.Mock)]
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Used by reflection. Cannot be static.")]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetMock(string driverBinaries)
        {
            return new MockWebDriver(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Safari)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetSafari(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<SafariOptions, SafariOptionsParams>(null);
            var _service = GetService<SafariDriverService, SafariServiceParams>(driverBinaries);

            // factor web driver
            return GetLocal<SafariDriver>(options, _service, driverBinaries);
        }

        // REMOTE WEB DRIVERS (Appium included)
        [DriverMethod(Driver = Driver.Chrome, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteChrome(string driverBinaries)
        {
            return GetRemote<ChromeOptions, ChromeOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Firefox, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteFirefox(string driverBinaries)
        {
            return GetRemote<FirefoxOptions, FirefoxOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.InternetExplorer, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteInternetExplorer(string driverBinaries)
        {
            return GetRemote<InternetExplorerOptions, InternetExplorerOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Edge, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteEdge(string driverBinaries)
        {
            return GetRemote<EdgeOptions, EdgeOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = Driver.Android, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteAndroid(string driverBinaries)
        {
            return GetMobile<AppiumOptions, AppiumOptionsParams, AndroidDriver<IWebElement>>(driverBinaries, "Android");
        }

        [DriverMethod(Driver = Driver.iOS, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteIos(string driverBinaries)
        {
            return GetMobile<AppiumOptions, AppiumOptionsParams, IOSDriver<IWebElement>>(driverBinaries, "iOS");
        }

        [DriverMethod(Driver = Driver.Safari, RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteSafari(string driverBinaries)
        {
            return GetRemote<SafariOptions, SafariOptionsParams>(driverBinaries);
        }

        [DriverMethod(Driver = "UiaDriver", RemoteDriver = true)]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used by reflection.")]
        private IWebDriver GetRemoteDriver(string driverBinaries)
        {
            // get constructor arguments
            var options = GetOptions<UiaOptions, UiaOptionsParams>(platformName: "WINDOWS");
            var capabilities = GetCapabilities(options, _capabilities);

            // factor web driver
            return _commandTimeout == 0
                ? new UiaDriver(new Uri(driverBinaries), capabilities)
                : new UiaDriver(new Uri(driverBinaries), capabilities, TimeSpan.FromSeconds(_commandTimeout));
        }
        #endregion

        // TODO: add default constructor options when there are no binaries to load
        // Gets local web driver instance
        private static IWebDriver GetLocal<TDriver>(DriverOptions options, DriverService service, string driverBinaries) where TDriver : IWebDriver
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
            var capabilities = GetCapabilities(options, _capabilities);

            // factor web driver
            return _commandTimeout == 0
                ? new RemoteWebDriver(new Uri(driverBinaries), capabilities)
                : new RemoteWebDriver(new Uri(driverBinaries), capabilities, TimeSpan.FromSeconds(_commandTimeout));
        }

        // parse driver-parameters and driver-options for remote web-driver
        private DriverOptions GetOptions<TOptions, TParams>(string platformName)
            where TOptions : DriverOptions, new()
            where TParams : DriverOptionsParams, IOptionable<TOptions>
        {
            // parse token
            var isOptions = this._options.Keys.Count > 0;

            // null validation
            if (!isOptions)
            {
                return new TOptions
                {
                    PlatformName = platformName
                };
            }

            // deserialize options
            var paramsObj = this._options.Transform<TParams>();

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
            if (service?.Any() != true)
            {
                return default;
            }

            // deserialize options
            var paramsObj = driverParams.Transform<TParams>();
            paramsObj.HostName = string.IsNullOrEmpty(paramsObj.HostName) ? Environment.MachineName : paramsObj.HostName;

            // return dynamic object
            return paramsObj.ToDriverService(driverBinaries);
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

        // Gets remote mobile web driver instance
        private IWebDriver GetMobile<TOptions, TParams, TDriver>(string driverBinaries, string platformName)
            where TOptions : AppiumOptions, new()
            where TParams : DriverOptionsParams, IOptionable<TOptions>
            where TDriver : AppiumDriver<IWebElement>
        {
            // get constructor arguments
            var options = GetOptions<TOptions, TParams>(platformName);
            GetCapabilities(options, _capabilities);

            var arguments = _commandTimeout == 0
                ? new object[] { new Uri(driverBinaries), options }
                : new object[] { new Uri(driverBinaries), options, TimeSpan.FromSeconds(_commandTimeout) };

            // factor web driver
            return (IWebDriver)Activator.CreateInstance(typeof(TDriver), arguments);
        }

        // get capabilities interface and manipulate capabilities
        private static ICapabilities GetCapabilities(DriverOptions driverOptions, IDictionary<string, object> rawCapabilities)
        {
            // convert options
            var options = driverOptions.ToCapabilities();

            // get capabilities field
            var isFieldNull = options
                .GetType()
                .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance) == null;
            var isUia = options.GetType().IsAssignableFrom(typeof(UiaCapabilities));

            Dictionary<string, object> cap = default;
            if (isUia)
            {
                cap = ((UiaCapabilities)options).CapabilitiesDictionary;
            }
            else if (isFieldNull)
            {
                cap = (Dictionary<string, object>)options
                    .GetType()
                    .BaseType
                    .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(options);
            }
            else if (!isFieldNull)
            {
                cap = (Dictionary<string, object>)options
                    .GetType()
                    .GetField("capabilities", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(options);
            }

            // exit conditions
            if (cap == default || rawCapabilities == null)
            {
                return options;
            }

            // add capabilities
            foreach (var item in rawCapabilities)
            {
                cap[item.Key] = ((JsonElement)item.Value).GetRawText();
            }
            return options;
        }
    }
}
