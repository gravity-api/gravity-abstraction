using OpenQA.Selenium.Remote;
using OpenQA.Selenium;

using System;
using System.Reflection;

namespace Gravity.Abstraction.Extensions
{
    internal static class WebDriverExtension
    {
        public static string GetSession(this IWebDriver driver)
        {
            return driver is IHasSessionId id ? $"{id.SessionId}" : $"{Guid.NewGuid()}";
        }

        public static Uri GetEndpoint(this IWebDriver driver)
        {
            // setup
            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

            // get RemoteWebDriver type
            var remoteWebDriver = GetRemoteWebDriver(driver.GetType());

            // get this instance executor > get this instance internalExecutor
            var executor = remoteWebDriver.GetField("executor", Flags).GetValue(driver) as ICommandExecutor;

            // get URL
            var endpoint = executor.GetType().GetField("service", Flags).GetValue(executor) as DriverService;

            // result
            return endpoint.ServiceUrl;
        }

        private static Type GetRemoteWebDriver(Type type)
        {
            // if not a remote web driver, return the type used for the call
            if (!typeof(RemoteWebDriver).IsAssignableFrom(type))
            {
                return type;
            }

            // iterate until gets the RemoteWebDriver type
            while (type != typeof(RemoteWebDriver))
            {
                type = type.BaseType;
            }

            // gets RemoteWebDriver to use for extracting internal information
            return type;
        }
    }
}
