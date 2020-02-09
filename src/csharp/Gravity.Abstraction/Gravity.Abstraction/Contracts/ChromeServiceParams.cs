/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 */
using System.Runtime.Serialization;
using OpenQA.Selenium.Chrome;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity chrome-service information to gravity-service
    /// </summary>
    [DataContract]
    public class ChromeServiceParams : DriverServiceParams, IServiceable<ChromeDriverService>
    {
        /// <summary>
        /// gets or sets the port on which the Android Debug Bridge is listening for commands
        /// </summary>
        /// <value>the android debug bridge port</value>
        [DataMember]
        public int AndroidDebugBridgePort { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether to enable verbose logging for the chrome-driver
        /// executable. defaults to false
        /// </summary>
        [DataMember]
        public bool EnableVerboseLogging { get; set; }

        /// <summary>
        /// gets or sets the location of the log file written to by the ChromeDriver executable
        /// </summary>
        [DataMember]
        public string LogPath { get; set; }

        /// <summary>
        /// gets or sets the address of a server to contact for reserving a port
        /// </summary>
        [DataMember]
        public string PortServerAddress { get; set; }

        /// <summary>
        /// gets or sets the base URL path prefix for commands (e.g., "wd/url").
        /// </summary>
        [DataMember]
        public string UrlPathPrefix { get; set; }

        /// <summary>
        /// gets or sets the comma-delimited list of IP addresses that are approved to connect
        /// to this instance of the chrome driver. defaults to an empty string, which means
        /// only the local loop-back address can connect
        /// </summary>
        [DataMember]
        public string WhitelistedIPAddresses { get; set; }

        /// <summary>
        /// generate driver-service for the current driver based of the params object
        /// </summary>
        /// <param name="driverPath">the full path to the directory containing the executable providing the service
        /// to drive the browser</param>
        /// <returns>chrome driver-service</returns>
        public ChromeDriverService ToDriverService(string driverPath)
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(driverPath);

            LoadBaseDriverService(chromeDriverService);                          // pipe: step #1
            chromeDriverService.AndroidDebugBridgePort = AndroidDebugBridgePort; // pipe: step #2
            chromeDriverService.EnableVerboseLogging = EnableVerboseLogging;     // pipe: step #3
            chromeDriverService.LogPath = LogPath;                               // pipe: step #4
            chromeDriverService.PortServerAddress = PortServerAddress;           // pipe: step #5
            chromeDriverService.UrlPathPrefix = UrlPathPrefix;                   // pipe: step #6
            chromeDriverService.WhitelistedIPAddresses = WhitelistedIPAddresses; // pipe: step #7

            // complete pipe
            return chromeDriverService;
        }
    }
}