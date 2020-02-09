/*
 * CHANGE LOG (keep only last 5 threads)
 *    
 * 2019-01-17
 *    - modify: improve XML comments
 *    - modify: set default string.empty value using constructor
 */
using System;

namespace Gravity.Abstraction.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    internal class DriverMethodAttribute : Attribute
    {
        public DriverMethodAttribute()
        {
            Driver = string.Empty;
            RemoteDriver = string.Empty;
        }

        /// <summary>
        /// gets or set the driver type
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// gets or sets the driver type under the context of RemoteWebDriver
        /// </summary>
        public string RemoteDriver { get; set; }
    }
}