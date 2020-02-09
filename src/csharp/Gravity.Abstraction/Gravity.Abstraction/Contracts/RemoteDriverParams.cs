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
    /// describes a contract for sending gravity remote driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class RemoteDriverParams : DriverParams<DriverOptionsParams, DriverServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => Remote;

        /// <summary>
        /// gets or sets the remote type
        /// </summary>
        [DataMember]
        public string RemoteDriver { get; set; }

        /// <summary>
        /// gets or sets internet-explorer driver-options
        /// </summary>
        [DataMember]
        public override DriverOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets remote driver-service configuration
        /// </summary>
        [DataMember]
        public override DriverServiceParams Service { get; set; }
    }
}