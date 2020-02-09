/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: driver type is now string
 *    - modify: add xml comments
 *    - modify: name-space simplified
 */
using System.Runtime.Serialization;
using static Gravity.Abstraction.Contracts.Driver;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity chrome-driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class ChromeDriverParams : DriverParams<ChromeOptionsParams, ChromeServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => Chrome;

        /// <summary>
        /// gets or sets chrome driver-options
        /// </summary>
        [DataMember]
        public override ChromeOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets chrome driver-service configuration
        /// </summary>
        [DataMember]
        public override ChromeServiceParams Service { get; set; }
    }
}