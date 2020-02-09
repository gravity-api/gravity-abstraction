/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: driver type is now string
 *    - modify: add xml comments
 *    - modify: name-space simplified
 */
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity appium-driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class AppiumDriverParams : DriverParams<AppiumOptionsParams, AppiumServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => Contracts.Driver.Appium;

        /// <summary>
        /// gets or sets appium driver-options
        /// </summary>
        [DataMember]
        public override AppiumOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets appium driver-service configuration
        /// </summary>
        [DataMember]
        public override AppiumServiceParams Service { get; set; }
    }
}