
using Gravity.Abstraction.Interfaces;

using Newtonsoft.Json;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Uia;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity edge-options to gravity-service
    /// </summary>
    [DataContract]
    public class UiaOptionsParams : DriverOptionsParams, IOptionable<UiaOptions>
    {
        // members: state
        private UiaOptions uiaOptions;

        /// <summary>
        /// gets or sets path to application file
        /// </summary>
        [DataMember]
        public string Application { get; set; }

        /// <summary>
        /// gets or sets command line arguments to pass to the application
        /// </summary>
        [DataMember]
        public IEnumerable<string> Arguments { get; set; }

        /// <summary>
        /// sets or sets value that specify the scope of elements within the UI Automation tree
        /// </summary>
        [DataMember]
        public TreeScope TreeScope { get; set; }

        /// <summary>
        /// gets or sets the option to refresh application DOM
        /// on element interactions. when set to true, this option will provide
        /// more updated and accurate DOM, but will reduce performance. use it for
        /// development and set it to false when deploy to production
        /// </summary>
        [DataMember]
        public bool DevelopmentMode { get; set; }

        /// <summary>
        /// the full path to the directory containing UiaWebDriver.exe
        /// </summary>
        [DataMember]
        public string DriverPath { get; set; } = Environment.CurrentDirectory;

        /// <summary>
        /// Prefer native events over patterns when possible.
        /// </summary>
        [DataMember]
        public bool UseNativeEvents { get; set; }

        /// <summary>
        /// generate driver-options for the current driver based of the params object
        /// </summary>
        /// <returns>chrome driver-options</returns>
        public UiaOptions ToDriverOptions()
        {
            uiaOptions = new UiaOptions();

            ToDriverOptions(uiaOptions);
            uiaOptions.Arguments = Arguments;
            uiaOptions.TreeScope = TreeScope;
            uiaOptions.DriverPath = DriverPath;
            uiaOptions.UseNativeEvents = UseNativeEvents;
            uiaOptions.Application = Application;

            return uiaOptions;
        }
    }
}
