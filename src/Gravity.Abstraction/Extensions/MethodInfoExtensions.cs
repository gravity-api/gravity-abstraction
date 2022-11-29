using Gravity.Abstraction.Attributes;
using OpenQA.Selenium;
using System.Reflection;

namespace Gravity.Abstraction.Extensions
{
    internal static class MethodInfoExtensions
    {
        /// <summary>
        /// asserts that a method is web-driver method
        /// </summary>
        /// <param name="method">method information to assert</param>
        /// <returns>true if match; false if not</returns>
        public static bool IsDriverMethod(this MethodInfo method)
        {
            // setup conditions
            var isWebDriver = method.ReturnType == typeof(IWebDriver);
            var isMethod = method.GetCustomAttribute<DriverMethodAttribute>() != null;

            // assert
            return isWebDriver && isMethod;
        }

        /// <summary>
        /// assert that method have the specified driver types
        /// </summary>
        /// <param name="method">method information to assert</param>
        /// <param name="driver">driver name</param>
        /// <param name="isRemote">remote driver name</param>
        /// <returns>true if match; false if not</returns>
        public static bool DriverMatch(this MethodInfo method, string driver, bool isRemote)
        {
            // normalize arguments
            driver ??= string.Empty;

            // get attribute
            var attribute = method.GetCustomAttribute<DriverMethodAttribute>();

            // setup conditions
            var isDriver = attribute?.Driver == driver;
            var isRemoteDriver = attribute?.RemoteDriver == isRemote;

            // assert
            return isDriver && isRemoteDriver;
        }
    }
}
