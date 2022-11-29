/*
 * CHANGE LOG - keep only last 5 threads
 * 
 */
using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium.Safari;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// Describes a contract for sending gravity Safari options to gravity service.
    /// </summary>
    [DataContract]
    public class SafariOptionsParams : DriverOptionsParams, IOptionable<SafariOptions>
    {
        /// <summary>
        /// Gets or sets a value indicating whether to have the driver preload the Web Inspector
        /// and JavaScript debugger in the background.
        /// </summary>
        [DataMember]
        public bool EnableAutomaticInspection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to have the driver preload the Web Inspector
        /// and start a time line recording in the background.
        /// </summary>
        [DataMember]
        public bool EnableAutomaticProfiling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the browser is the technology preview.
        /// </summary>
        [DataMember]
        public bool IsTechnologyPreview { get; set; }

        /// <summary>
        /// Generate driver options for the current driver based of the parameters object.
        /// </summary>
        /// <returns>Safari driver options.</returns>
        public SafariOptions ToDriverOptions() => new SafariOptions
        {
            EnableAutomaticInspection = EnableAutomaticInspection,
            EnableAutomaticProfiling = EnableAutomaticProfiling,
            IsTechnologyPreview = IsTechnologyPreview
        };
    }
}