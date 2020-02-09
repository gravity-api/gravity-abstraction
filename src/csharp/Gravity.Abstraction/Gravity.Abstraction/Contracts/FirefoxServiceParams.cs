using System.Runtime.Serialization;
using OpenQA.Selenium.Firefox;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    [DataContract]
    public class FirefoxServiceParams : DriverServiceParams, IServiceable<FirefoxDriverService>
    {
        /// <summary>
        /// Gets or sets the port used by the driver executable to communicate with the browser.
        /// </summary>
        [DataMember]
        public int BrowserCommunicationPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to connect to an already-running instance
        /// of Firefox.
        /// </summary>
        [DataMember]
        public bool ConnectToRunningBrowser { get; set; }

        /// <summary>
        /// Gets or sets the location of the Firefox binary executable.
        /// </summary>
        [DataMember]
        public string FirefoxBinaryPath { get; set; }

        /// <summary>
        /// Gets or sets the value of the IP address of the host adapter on which the service
        /// should listen for connections.
        /// </summary>
        [DataMember]
        public string Host { get; set; }

        /// <summary>
        /// Generate DriverService for the current driver based of the params object
        /// </summary>
        /// <param name="driverPath">The full path to the directory containing the executable providing the service
        /// to drive the browser.</param>
        /// <returns>DriverService</returns>
        public FirefoxDriverService ToDriverService(string driverPath)
        {
            var firefoxDriverService = FirefoxDriverService.CreateDefaultService(driverPath);

            LoadBaseDriverService(firefoxDriverService);                              // pipe: step #1
            firefoxDriverService.BrowserCommunicationPort = BrowserCommunicationPort; // pipe: step #2
            firefoxDriverService.ConnectToRunningBrowser = ConnectToRunningBrowser;   // pipe: step #3
            firefoxDriverService.FirefoxBinaryPath = FirefoxBinaryPath;               // pipe: step #4
            firefoxDriverService.Host = Host;                                         // pipe: step #5

            // complete pipe
            return firefoxDriverService;
        }
    }
}