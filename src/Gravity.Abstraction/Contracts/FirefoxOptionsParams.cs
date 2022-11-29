using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenQA.Selenium.Firefox;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    [DataContract]
    public class FirefoxOptionsParams : DriverOptionsParams, IOptionable<FirefoxOptions>
    {
        private FirefoxOptions m_firefoxOptions; // will be returned as firefox-options for this instance

        /// <summary>
        /// Adds a list arguments to be used in launching the Firefox browser.
        /// </summary>
        [DataMember]
        public string[] Arguments { get; set; }

        /// <summary>
        /// Gets or sets the path and file name of the Firefox browser executable.
        /// </summary>
        [DataMember]
        public string BrowserExecutableLocation { get; set; }

        /// <summary>
        /// Gets or sets the logging level of the Firefox driver.
        /// </summary>
        [DataMember]
        public FirefoxDriverLogLevel LogLevel { get; set; }

        /// <summary>
        /// Sets a preference in the profile used by Firefox.
        /// </summary>
        [DataMember]
        public KeyValuePair<string, bool>[] Preferences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the legacy driver implementation.
        /// </summary>
        [DataMember]
        public bool UseLegacyImplementation { get; set; }

        /// <summary>
        /// Generate DriverOptions for the current driver based of the params object
        /// </summary>
        /// <returns>DriverOptions</returns>
        public FirefoxOptions ToDriverOptions()
        {
            m_firefoxOptions = new FirefoxOptions();                                // initialize response

            ToDriverOptions(m_firefoxOptions);                                      // pipe: step #1
            LoadArguments();                                                        // pipe: step #2
            m_firefoxOptions.BrowserExecutableLocation = BrowserExecutableLocation; // pipe: step #3
            m_firefoxOptions.LogLevel = LogLevel;                                   // pipe: step #4
            LoadPreference();                                                       // pipe: step #5
            m_firefoxOptions.UseLegacyImplementation = UseLegacyImplementation;     // pipe: step #6

            return m_firefoxOptions;                                                // complete pipe
        }

        private void LoadArguments()
        {
            // exit conditions
            if (Arguments?.Length == 0 || Arguments == null) return;

            // append
            m_firefoxOptions.AddArguments(Arguments);
        }

        private void LoadPreference()
        {
            // exit conditions
            if (Preferences?.Length == 0 || Preferences == null) return;

            // append
            foreach (var preference in Preferences)
            {
                m_firefoxOptions.SetPreference(preference.Key, preference.Value);
            }
        }
    }
}