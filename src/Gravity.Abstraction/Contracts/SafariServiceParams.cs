/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium.Safari;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// Describes a contract for sending gravity Safari service information to gravity service.
    /// </summary>
    [DataContract]
    public class SafariServiceParams : DriverServiceParams, IServiceable<SafariDriverService>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to use the default open-source project
        /// dialect of the protocol instead of the default dialect compliant with the W3C
        /// WebDriver Specification.
        /// </summary>
        /// 
        /// <remarks>
        /// This is only valid for versions of the driver for Safari that target Safari 12
        /// or later, and will result in an error if used with prior versions of the driver.
        /// </remarks>
        public bool UseLegacyProtocol { get; set; }

        /// <summary>
        /// Generate driver service for the current driver based on the parameters object.
        /// </summary>
        /// <param name="driverPath">The full path to the directory containing the executable providing the service
        /// to drive the browser</param>
        /// <returns>Safari driver service.</returns>
        public SafariDriverService ToDriverService(string driverPath)
        {
            var safariDriverService = SafariDriverService.CreateDefaultService(driverPath);

            LoadBaseDriverService(safariDriverService);                // pipe: step #1
            safariDriverService.UseLegacyProtocol = UseLegacyProtocol; // pipe: step #2

            // complete pipe
            return safariDriverService;
        }
    }
}