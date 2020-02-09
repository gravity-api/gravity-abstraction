/*
 * CHANGE LOG
 * 
 * 2019-01-10
 *    - modify: xml comments
 *    - modify: add serialization attributes
 */
using OpenQA.Selenium;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// base contract for sending gravity service-params to gravity-service
    /// </summary>
    [DataContract]
    public abstract class DriverServiceParams
    {
        /// <summary>
        /// gets or sets a value indicating whether the command prompt window of the service
        /// should be hidden
        /// </summary>
        [DataMember]
        public bool HideCommandPromptWindow { get; set; }

        /// <summary>
        /// gets or sets the port of the service
        /// </summary>
        [DataMember]
        public int Port { get; set; }

        /// <summary>
        /// gets or sets the host name of the service. defaults to "localhost"
        /// </summary>
        /// <remarks>
        /// most driver service executables do not allow connections from remote (non-local)
        /// machines. this property can be used as a workaround so that an IP address (like
        /// "127.0.0.1" or "::1") can be used instead
        /// </remarks>
        [DataMember]
        public string HostName { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether the initial diagnostic information is
        /// suppressed when starting the driver server executable. defaults to false, meaning
        /// diagnostic information should be shown by the driver server executable
        /// </summary>
        [DataMember]
        public bool SuppressInitialDiagnosticInformation { get; set; }

        // load all information into the current driver-service instance
        internal T LoadBaseDriverService<T>(T driverService) where T : DriverService
        {
            driverService.HideCommandPromptWindow = HideCommandPromptWindow;
            driverService.Port = Port;
            driverService.HostName = HostName;
            driverService.SuppressInitialDiagnosticInformation = SuppressInitialDiagnosticInformation;

            return driverService;
        }
    }
}