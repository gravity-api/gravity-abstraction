/*
 * CHANGE LOG (keep only last 5 threads)
 */
using System;

namespace Gravity.Abstraction.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DriverMethodAttribute : Attribute
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
