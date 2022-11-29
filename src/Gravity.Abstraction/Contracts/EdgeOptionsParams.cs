/*
 * CHANGE LOG
 * 
 * 2019-01-10
 *    - modify: add xml comments
 */
using System.Runtime.Serialization;
using OpenQA.Selenium.Edge;
using Gravity.Abstraction.Interfaces;
using Gravity.Abstraction.Contracts;

namespace  Gravity.Services.DataContracts
{
    /// <summary>
    /// describes a contract for sending gravity edge-options to gravity-service
    /// </summary>
    [DataContract]
    public class EdgeOptionsParams : DriverOptionsParams, IOptionable<EdgeOptions>
    {
        /// <summary>
        /// generate driver-options for the current driver based of the params object
        /// </summary>
        /// <returns>chrome driver-options</returns>
        public EdgeOptions ToDriverOptions() => ToDriverOptions(new EdgeOptions());
    }
}