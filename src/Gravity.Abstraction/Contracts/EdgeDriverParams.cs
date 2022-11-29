/*
 * CHANGE LOG
 * 
 * 2019-01-10
 *    - modify: driver is now string
 *    - modify: add xml comments
 *    -    fix: all warnings
 */
using Gravity.Services.DataContracts;
using System.Runtime.Serialization;
using static Gravity.Abstraction.Contracts.Driver;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity edge-driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class EdgeDriverParams : DriverParams<EdgeOptionsParams, EdgeServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => Edge;

        /// <summary>
        /// gets or sets edge driver-options
        /// </summary>
        [DataMember]
        public override EdgeOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets edge driver-service configuration
        /// </summary>
        [DataMember]
        public override EdgeServiceParams Service { get; set; }
    }
}