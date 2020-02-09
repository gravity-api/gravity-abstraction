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
    /// describes a contract for sending gravity internet-explorer driver-params to gravity-service
    /// </summary>
    [DataContract]
    public class InternetExplorerDriverParams
        : DriverParams<InternetExplorerOptionsParams, InternetExplorerServiceParams>
    {
        /// <summary>
        /// gets the driver type
        /// </summary>
        [DataMember]
        public override string Driver => InternetExplorer;

        /// <summary>
        /// gets or sets internet-explorer driver-options
        /// </summary>
        [DataMember]
        public override InternetExplorerOptionsParams Options { get; set; }

        /// <summary>
        /// gets or sets internet-explorer driver-service configuration
        /// </summary>
        [DataMember]
        public override InternetExplorerServiceParams Service { get; set; }
    }
}