/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenQA.Selenium;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity options to gravity-service
    /// </summary>
    [DataContract]
    public abstract class DriverOptionsParams
    {
        /// <summary>
        /// gets or sets the version of the browser
        /// </summary>
        [DataMember]
        public string BrowserVersion { get; set; }

        /// <summary>
        /// gets or sets the name of the platform on which the browser is running
        /// </summary>
        [DataMember]
        public string PlatformName { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether the browser should accept self-signed
        /// SSL certificates
        /// </summary>
        [DataMember]
        public bool? AcceptInsecureCertificates { get; set; }

        /// <summary>
        /// gets or sets the value for describing how unexpected alerts are to be handled
        /// in the browser. defaults to OpenQA.Selenium.UnhandledPromptBehavior.Default
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public UnhandledPromptBehavior UnhandledPromptBehavior { get; set; }

        /// <summary>
        /// gets or sets the value for describing how the browser is to wait for pages to
        /// load in the browser. defaults to OpenQA.Selenium.PageLoadStrategy.Default
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public PageLoadStrategy PageLoadStrategy { get; set; }

        /// <summary>
        /// gets or sets the OpenQA.Selenium.DriverOptions.Proxy to be used with this browser
        /// </summary>
        [DataMember]
        public Proxy Proxy { get; set; }

        /// <summary>
        /// provides a means to add additional capabilities not yet added as type safe options
        /// for the specific browser driver
        /// </summary>
        [DataMember]
        public Dictionary<string, object> AdditionalCapabilities { get; set; }

        /// <summary>
        /// abstract the given driver-options and populate it into the this driver-options instance
        /// </summary>
        /// <typeparam name="T">options type</typeparam>
        /// <param name="driverOptions">options object to populate</param>
        /// <returns>driver-options object</returns>
        internal T ToDriverOptions<T>(T driverOptions) where T : DriverOptions
        {
            // populate values
            LoadCapabilities(driverOptions);
            LoadBrowserVersion(driverOptions);
            LoadPlatformName(driverOptions);
            LoadUnhandledPromptBehavior(driverOptions);
            LoadPageLoadStrategy(driverOptions);
            LoadProxy(driverOptions);

            // Firefox/Internet-Explorer handler
            var isFirefox = string.Equals(typeof(T).Name, "FIREFOXOPTIONS", StringComparison.OrdinalIgnoreCase);
            var isExplorer = string.Equals(typeof(T).Name, "INTERNETEXPLOREROPTIONS", StringComparison.OrdinalIgnoreCase);
            if (!(isFirefox && isExplorer))
            {
                LoadAcceptInsecureCertificates(driverOptions);
            }

            // return populated options
            return driverOptions;
        }

        // load option: capabilities
        private void LoadCapabilities(DriverOptions driverOptions)
        {
            // exit conditions
            if (AdditionalCapabilities == null || AdditionalCapabilities.Count == 0) return;

            // append capabilities
            foreach (var capability in AdditionalCapabilities)
            {
                try
                {
                    driverOptions.AddAdditionalCapability(capability.Key, capability.Value);
                }
                catch (Exception e)
                {
                    Trace.TraceWarning(e.Message);
                }
            }
        }

        // load option: browser-version
        private void LoadBrowserVersion(DriverOptions driverOptions)
        {
            driverOptions.BrowserVersion = BrowserVersion;
        }

        // load option: platform-name
        private void LoadPlatformName(DriverOptions driverOptions)
        {
            driverOptions.PlatformName = PlatformName;
        }

        // load option: accept-insecure-certificates
        private void LoadAcceptInsecureCertificates(DriverOptions driverOptions)
        {
            driverOptions.AcceptInsecureCertificates = AcceptInsecureCertificates;
        }

        // load option: unhandled-prompt-behavior
        private void LoadUnhandledPromptBehavior(DriverOptions driverOptions)
        {
            driverOptions.UnhandledPromptBehavior = UnhandledPromptBehavior;
        }

        // load option: page-load-strategy
        private void LoadPageLoadStrategy(DriverOptions driverOptions)
        {
            driverOptions.PageLoadStrategy = PageLoadStrategy;
        }

        // load option: load-proxy
        private void LoadProxy(DriverOptions driverOptions)
        {
            driverOptions.Proxy = Proxy;
        }
    }
}