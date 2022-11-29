using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium.IE;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    [DataContract]
    public class InternetExplorerServiceParams : DriverServiceParams, IServiceable<InternetExplorerDriverService>
    {
        /// <summary>
        /// Gets or sets the value of the host adapter on which the IEDriverServer should
        /// listen for connections.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        [DataMember]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the path to which the supporting library of the IEDriverServer.exe
        /// is extracted. Defaults to the temp directory if this property is not set.
        /// </summary>
        /// <value>
        /// The library extraction path.
        /// </value>
        [DataMember]
        public string LibraryExtractionPath { get; set; }

        /// <summary>
        /// Gets or sets the location of the log file written to by the IEDriverServer.
        /// </summary>
        /// <value>
        /// The log file.
        /// </value>
        [DataMember]
        public string LogFile { get; set; }

        /// <summary>
        /// Gets or sets the logging level used by the IEDriverServer.
        /// </summary>
        /// <value>
        /// The logging level.
        /// </value>
        [DataMember]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public InternetExplorerDriverLogLevel LoggingLevel { get; set; }

        /// <summary>
        /// Gets or sets the comma-delimited list of IP addresses that are approved to connect
        /// to this instance of the IEDriverServer. Defaults to an empty string, which means
        /// only the local loop-back address can connect.
        /// </summary>
        /// <value>
        /// The white-listed IP addresses.
        /// </value>
        [DataMember]
        public string WhitelistedIPAddresses { get; set; }

        /// <summary>
        /// generate driver-service for the current driver based of the parameters object
        /// </summary>
        /// <param name="driverPath">the full path to the directory containing the executable providing the service
        /// to drive the browser</param>
        /// <returns>chrome driver-service</returns>
        public InternetExplorerDriverService ToDriverService(string driverPath)
        {
            var internetExplorerDriverService = InternetExplorerDriverService.CreateDefaultService(driverPath);

            LoadBaseDriverService(internetExplorerDriverService);                          // pipe: step #1
            internetExplorerDriverService.Host = Host;                                     // pipe: step #2
            internetExplorerDriverService.LibraryExtractionPath = LibraryExtractionPath;   // pipe: step #3
            internetExplorerDriverService.LogFile = LogFile;                               // pipe: step #4
            internetExplorerDriverService.LoggingLevel = LoggingLevel;                     // pipe: step #5
            internetExplorerDriverService.WhitelistedIPAddresses = WhitelistedIPAddresses; // pipe: step #6

            // complete pipe
            return internetExplorerDriverService;
        }
    }
}