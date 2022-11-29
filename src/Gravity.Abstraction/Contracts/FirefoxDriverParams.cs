/*
 * CHANGE LOG
 * 
 * 2019-01-10
 *    - modify: driver is now string
 *    - modify: add xml comments
 *    -    fix: all warnings
 */
using System.Runtime.Serialization;
using static Gravity.Abstraction.Contracts.Driver;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity firefox-driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class FirefoxDriverParams : DriverParams<FirefoxOptionsParams, FirefoxServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => Firefox;

        /// <summary>
        /// gets or sets firefox driver-options
        /// </summary>
        [DataMember]
        public override FirefoxOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets firefox driver-service configuration
        /// </summary>
        [DataMember]
        public override FirefoxServiceParams Service { get; set; }

        /// <summary>
        /// gets or sets firefox profile configuration
        /// </summary>
        [DataMember]
        public FirefoxProfileParams Profile { get; set; }
    }
}