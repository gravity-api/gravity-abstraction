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
    internal sealed class DriverMethodAttribute : Attribute
    {
        public DriverMethodAttribute()
        {
            Driver = string.Empty;
            RemoteDriver = false;
        }

        /// <summary>
        /// Get or sets the driver type based on <see cref="Contracts.Driver"/>.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets value indicating if this is a remote (using Grid) or local driver.
        /// </summary>
        public bool RemoteDriver { get; set; }
    }
}