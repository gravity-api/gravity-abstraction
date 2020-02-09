/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: driver type is now string
 *    - modify: add interfaces
 *    - modify: xml comments
 */
using System.Collections.Generic;
using System.Runtime.Serialization;
using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity driver-params to gravity-service
    /// </summary>
    /// <typeparam name="TOptions">options type which will be used under this driver-params</typeparam>
    /// <typeparam name="TService">service type which will be used under this driver-params</typeparam>
    [DataContract]
    public abstract class DriverParams<TOptions, TService> : IHasOptions<TOptions>, IHasService<TService>
        where TOptions : DriverOptionsParams
        where TService : DriverServiceParams
    {
        /// <summary>
        /// gets or sets the driver-binaries (i.e. path or remote address)
        /// </summary>
        [DataMember]
        public string DriverBinaries { get; set; }

        /// <summary>
        /// implicitly gets or sets the page load timeout, which is the amount of time the driver should
        /// wait for a page to load when setting the OpenQA.Selenium.IWebDriver.Url property
        /// </summary>
        [DataMember]
        public int PageLoadTimeout { get; set; }

        /// <summary>
        /// WebDriver capabilities are used to communicate the features supported by a given implementation or vendor.
        /// </summary>
        [DataMember]
        public Dictionary<string, object> Capabilities { get; set; }

        /// <summary>
        /// gets or sets a proxy to be used with this driver instance
        /// </summary>
        [DataMember]
        public Proxy Proxy { get; set; }

        /// <summary>
        /// gets or sets driver-options
        /// </summary>
        [DataMember]
        public abstract TOptions Options { get; set; }

        /// <summary>
        /// gets or sets driver-service configuration
        /// </summary>
        [DataMember]
        public abstract TService Service { get; set; }

        /// <summary>
        /// gets this driver type
        /// </summary>
        [DataMember]
        public abstract string Driver { get; }
    }
}