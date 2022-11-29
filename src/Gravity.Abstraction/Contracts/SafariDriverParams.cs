/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using System.Runtime.Serialization;
using static Gravity.Abstraction.Contracts.Driver;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// Describes a contract for sending gravity safari driver parameters to gravity service.
    /// </summary>
    [DataContract]
    public class SafariDriverParams : DriverParams<SafariOptionsParams, SafariServiceParams>
    {
        /// <summary>
        /// Gets the driver type.
        /// </summary>
        [DataMember]
        public override string Driver => Safari;

        /// <summary>
        /// Gets or sets Safari driver options.
        /// </summary>
        [DataMember]
        public override SafariOptionsParams Options { get; set; }

        /// <summary>
        /// Gets or sets Safari driver service configuration
        /// </summary>
        [DataMember]
        public override SafariServiceParams Service { get; set; }
    }
}