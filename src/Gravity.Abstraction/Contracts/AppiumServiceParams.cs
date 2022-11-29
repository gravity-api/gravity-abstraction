/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 *    - modify: IDictionary to Dictionary
 */
using Gravity.Abstraction.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity appium-service information to gravity-service
    /// </summary>
    [DataContract]
    public class AppiumServiceParams : DriverServiceParams, IServiceable<AppiumServiceBuilder>
    {
        // members: state
        private readonly AppiumServiceBuilder serviceBuilder;
        private OptionCollector options;

        /// <summary>
        /// create new instance of appium-service-parameters object
        /// </summary>
        public AppiumServiceParams()
        {
            serviceBuilder = new AppiumServiceBuilder();
        }

        /// <summary>
        /// configures the appium server to start on any available port
        /// </summary>
        [DataMember]
        public bool UsingAnyFreePort { get; set; }

        /// <summary>
        /// the executable Node.js to use
        /// </summary>
        [DataMember]
        public string DriverExecutable { get; set; }

        /// <summary>
        /// the executable Appium.js to use
        /// </summary>
        [DataMember]
        public string AppiumJs { get; set; }

        /// <summary>
        /// a file to write log to
        /// </summary>
        [DataMember]
        public string LogFile { get; set; }

        /// <summary>
        /// a time value for the service starting up
        /// </summary>
        [DataMember]
        public TimeSpan StartUpTimeout { get; set; }

        /// <summary>
        /// adds an argument and its value
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Arguments { get; set; }

        /// <summary>
        /// adds/merges server-specific capabilities
        /// </summary>
        [DataMember]
        public AppiumOptionsParams Capabilities { get; set; }

        /// <summary>
        /// environment variables to launch the appium server with
        /// </summary>
        [DataMember]
        public Dictionary<string, string> Environment { get; set; }

        /// <summary>
        /// generate driver-service for the current driver based of the params object
        /// </summary>
        /// <param name="driverPath">the full path to the file containing the executable providing the service to drive the browser</param>
        /// <returns>appium-service-builder</returns>
        public AppiumServiceBuilder ToDriverService(string driverPath)
        {
            // setup
            SetUsingAnyFreePort();
            SetDriverExecutable();
            SetPort();
            SetAppiumJs();
            SetLogFile();
            SetStartUpTimeout();
            SetArguments();
            SetCapabilities();
            SetEnvironment();
            SetHostName();

            // return populated builder
            return serviceBuilder;
        }

        // handler: using-any-free-port
        private void SetUsingAnyFreePort()
        {
            // exit conditions
            if (!UsingAnyFreePort) return;

            // set property
            serviceBuilder.UsingAnyFreePort();
        }

        // handler: driver-executable
        private void SetDriverExecutable()
        {
            // exit conditions
            if (string.IsNullOrEmpty(DriverExecutable)) return;

            // set property
            serviceBuilder.UsingDriverExecutable(new FileInfo(DriverExecutable));
        }

        // handler: port
        private void SetPort()
        {
            // exit conditions
            if (Port == default) return;

            // set property
            serviceBuilder.UsingPort(Port);
        }

        // handler: appium-js
        private void SetAppiumJs()
        {
            // exit conditions
            if (string.IsNullOrEmpty(AppiumJs)) return;

            // set property
            serviceBuilder.WithAppiumJS(new FileInfo(AppiumJs));
        }

        // handle: log-file
        private void SetLogFile()
        {
            // exit conditions
            if (string.IsNullOrEmpty(LogFile)) return;

            // set property
            serviceBuilder.WithLogFile(new FileInfo(LogFile));
        }

        // handler: start-up-timeout
        private void SetStartUpTimeout()
        {
            // exit conditions
            if (StartUpTimeout == default) return;

            // set property
            serviceBuilder.WithStartUpTimeOut(StartUpTimeout);
        }

        // handler: arguments
        private void SetArguments()
        {
            // exit conditions
            var compliance = Arguments?.Count > 0;
            if (!compliance) return;

            // set properties
            options ??= new OptionCollector();
            foreach (var argumet in Arguments)
            {
                var argumentOption = new KeyValuePair<string, string>(argumet.Key, argumet.Value);
                options.AddArguments(argumentOption);
            }
            serviceBuilder.WithArguments(options);
        }

        // handler: capabilities
        private void SetCapabilities()
        {
            // exit conditions
            if (Capabilities == null) return;

            // set properties
            options ??= new OptionCollector();
            options.AddCapabilities(Capabilities.ToDriverOptions());
            serviceBuilder.WithArguments(options);
        }

        // handler: environment
        private void SetEnvironment()
        {
            // exit conditions
            var compliance = Environment?.Count > 0;
            if (!compliance) return;

            // set properties
            serviceBuilder.WithEnvironment(Environment);
        }

        // handler: set host-name
        private void SetHostName()
        {
            // exit conditions
            if (string.IsNullOrEmpty(HostName)) return;

            // set property
            serviceBuilder.WithIPAddress(HostName);
        }
    }
}
