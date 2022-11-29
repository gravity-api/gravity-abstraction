/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 */
using System.Runtime.Serialization;
using OpenQA.Selenium.Edge;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity edge-service information to gravity-service
    /// </summary>
    [DataContract]
    public class EdgeServiceParams : DriverServiceParams, IServiceable<EdgeDriverService>
    {
        /// <summary>
        /// gets or sets the value of the host adapter on which the Edge driver service should
        /// listen for connections
        /// </summary>
        [DataMember]
        public string Host { get; set; }

        /// <summary>
        /// gets or sets the value of the package the edge driver service will launch and
        /// automate
        /// </summary>
        [DataMember]
        public string Package { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether the service should use verbose logging.
        /// </summary>
        [DataMember]
        public bool UseVerboseLogging { get; set; }

        /// <summary>
        /// generate DriverService for the current driver based of the params object
        /// </summary>
        /// <param name="driverPath">the full path to the directory containing the executable providing the service
        /// to drive the browser</param>
        /// <returns>edge-driver-service</returns>
        public EdgeDriverService ToDriverService(string driverPath)
        {
            var edgeDriverService = EdgeDriverService.CreateDefaultService(driverPath);

            LoadBaseDriverService(edgeDriverService);                // pipe: step #1
            edgeDriverService.Host = Host;                           // pipe: step #2
            edgeDriverService.Package = Package;                     // pipe: step #3
            edgeDriverService.UseVerboseLogging = UseVerboseLogging; // pipe: step #4

            // complete pipe
            return edgeDriverService;
        }
    }
}