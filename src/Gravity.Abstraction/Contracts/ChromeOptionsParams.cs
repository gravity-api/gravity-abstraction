/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: add xml comments
 *    - modify: name-space simplified
 *    - modify: change KeyValuePair<string, object>[] to Dictionary<string, object>
 */
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenQA.Selenium.Chrome;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity chrome-options to gravity-service
    /// </summary>
    [DataContract]
    public class ChromeOptionsParams : DriverOptionsParams, IOptionable<ChromeOptions>
    {
        // members: state
        private ChromeOptions chromeOptions;

        /// <summary>
        /// adds arguments to be appended to the chrome.exe command line
        /// </summary>
        [DataMember]
        public string[] Arguments { get; set; }

        /// <summary>
        /// adds a list of base64-encoded strings representing Chrome extensions to the list
        /// of extensions to be installed in the instance of chrome
        /// </summary>
        [DataMember]
        public string[] EncodedExtensions { get; set; }

        /// <summary>
        /// adds arguments to be excluded from the list of arguments passed by default to
        /// the chrome.exe command line by chromedriver.exe
        /// </summary>
        [DataMember]
        public string[] ExcludedArguments { get; set; }

        /// <summary>
        /// adds a list of paths to packed Chrome extensions (.crx files) to be installed
        /// in the instance of Chrome
        /// </summary>
        [DataMember]
        public string[] Extensions { get; set; }

        /// <summary>
        /// adds a preference for the local state file in the user's data directory for chrome.
        /// if the specified preference already exists, it will be overwritten
        /// </summary>
        [DataMember]
        public Dictionary<string, object> LocalStatePreference { get; set; }

        /// <summary>
        /// adds a preference for the user-specific profile or "user data directory". if
        /// the specified preference already exists, it will be overwritten
        /// </summary>
        [DataMember]
        public Dictionary<string, object> UserProfilePreference { get; set; }

        /// <summary>
        /// adds a list of window types that will be listed in the list of window handles
        /// returned by the Chrome driver
        [DataMember]
        public string[] WindowTypes { get; set; }

        /// <summary>
        /// the name of the device to emulate. the device name must be a valid device name
        /// from the Chrome DevTools Emulation panel
        /// </summary>
        [DataMember]
        public string EnableMobileEmulation { get; set; }

        /// <summary>
        /// gets or sets a value indicating whether Chrome should be left running after the
        /// chrome-driver instance is exited. defaults to false.
        /// </summary>
        [DataMember]
        public bool LeaveBrowserRunning { get; set; }

        /// <summary>
        /// gets or sets the directory in which to store mini-dump files
        /// </summary>
        [DataMember]
        public string MinidumpPath { get; set; }

        /// <summary>
        /// generate driver-options for the current driver based of the params object
        /// </summary>
        /// <returns>chrome driver-options</returns>
        public ChromeOptions ToDriverOptions()
        {
            chromeOptions = new ChromeOptions();                        // initialize response

            ToDriverOptions(chromeOptions);                             // pipe: step #1
            LoadArguments();                                            // pipe: step #2
            LoadEncodedExtensions();                                    // pipe: step #3
            LoadExcludedArguments();                                    // pipe: step #4
            LoadExtensions();                                           // pipe: step #5
            LoadLocalStatePreference();                                 // pipe: step #6
            LoadUserProfilePreference();                                // pipe: step #7
            LoadWindowTypes();                                          // pipe: step #8
            chromeOptions.EnableMobileEmulation(EnableMobileEmulation); // pipe: step #9
            chromeOptions.LeaveBrowserRunning = LeaveBrowserRunning;    // pipe: step #10
            chromeOptions.MinidumpPath = MinidumpPath;                  // pipe: step #11

            return chromeOptions;                                       // complete pipe
        }

        // load option: arguments
        private void LoadArguments()
        {
            // exit conditions
            if (Arguments?.Length == 0 || Arguments == null) return;

            // append
            chromeOptions.AddArguments(Arguments);
        }

        // load option: encoded-extensions
        private void LoadEncodedExtensions()
        {
            // exit conditions
            if (EncodedExtensions?.Length == 0 || EncodedExtensions == null) return;

            // append
            chromeOptions.AddEncodedExtensions(EncodedExtensions);
        }

        // load option: excluded-arguments
        private void LoadExcludedArguments()
        {
            // exit conditions
            if (ExcludedArguments?.Length == 0 || ExcludedArguments == null) return;

            // append
            chromeOptions.AddExcludedArguments(ExcludedArguments);
        }

        // load option: extensions
        private void LoadExtensions()
        {
            // exit conditions
            if (Extensions?.Length == 0 || Extensions == null) return;

            // append
            chromeOptions.AddExtensions(Extensions);
        }

        // load option: local-state-preference
        private void LoadLocalStatePreference()
        {
            // exit conditions
            if (LocalStatePreference?.Keys.Count == 0 || LocalStatePreference == null) return;

            // append
            foreach (var preference in LocalStatePreference)
            {
                chromeOptions.AddLocalStatePreference(preference.Key, preference.Value);
            }
        }

        // load option: user-profile-preference
        private void LoadUserProfilePreference()
        {
            // exit conditions
            if (UserProfilePreference?.Keys.Count == 0 || UserProfilePreference == null) return;

            // append
            foreach (var preference in UserProfilePreference)
            {
                chromeOptions.AddUserProfilePreference(preference.Key, preference.Value);
            }
        }

        // load option: window-types
        private void LoadWindowTypes()
        {
            // exit conditions
            if (WindowTypes?.Length == 0 || WindowTypes == null) return;

            // append
            chromeOptions.AddWindowTypes(WindowTypes);
        }
    }
}