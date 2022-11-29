/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 *    - modify: name-space simplified
 */
using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium.Appium;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity appium-options to gravity-service
    /// </summary>
    [DataContract]
    public class AppiumOptionsParams : DriverOptionsParams, IOptionable<AppiumOptions>
    {
        /// <summary>
        /// generate driver-options for the current driver based of the parameters object
        /// </summary>
        /// <returns>driver-options</returns>
        public AppiumOptions ToDriverOptions() => ToDriverOptions(new AppiumOptions());
    }
}
