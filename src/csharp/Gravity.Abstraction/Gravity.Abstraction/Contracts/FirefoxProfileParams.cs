using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenQA.Selenium.Firefox;

namespace Gravity.Abstraction.Contracts
{
    [DataContract]
    public class FirefoxProfileParams
    {
        /// <summary>
        /// The directory containing the profile.
        /// </summary>
        [DataMember]
        public string ProfileDirectory { get; set; }

        /// <summary>
        /// Delete the source directory of the profile upon cleaning.
        /// </summary>
        [DataMember]
        public bool DeleteSourceOnClean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Firefox should accept SSL certificates
        /// which have expired, signed by an unknown authority or are generally untrusted.
        /// Set to true by default.
        /// </summary>
        [DataMember]
        public bool AcceptUntrustedCertificates { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to always load the library for allowing
        /// Firefox to execute commands without its window having focus.
        /// </summary>
        [DataMember]
        public bool AlwaysLoadNoFocusLibrary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Firefox assume untrusted SSL certificates
        /// come from an untrusted issuer or are self-signed. Set to true by default.
        /// </summary>
        [DataMember]
        public bool AssumeUntrustedCertificateIssuer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to delete this profile after use with
        /// the OpenQA.Selenium.Firefox.FirefoxDriver.
        /// </summary>
        [DataMember]
        public bool DeleteAfterUse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether native events are enabled.
        /// </summary>
        [DataMember]
        public bool EnableNativeEvents { get; set; }

        /// <summary>
        /// Gets or sets the port on which the profile connects to the WebDriver extension.
        /// </summary>
        [DataMember]
        public int Port { get; set; }

        /// <summary>
        /// Adds a Firefox Extension to this profile.
        /// </summary>
        [DataMember]
        public string[] Extensions { get; set; }

        /// <summary>
        /// Sets a preference in the profile.
        /// </summary>
        [DataMember]
        public KeyValuePair<string, object> Preferences { get; set; }

        /// <summary>
        /// generate new firefox-profile based on the given profile-params
        /// </summary>
        /// <returns>FirefoxProfile</returns>
        public FirefoxProfile ToProfile()
        {
            // pipe: step #1: create profile based on arguments
            var firefoxProfile = ResetProfile();

            // pipe: step #2
            firefoxProfile.AcceptUntrustedCertificates = AcceptUntrustedCertificates;

            // pipe: step #3
            firefoxProfile.AlwaysLoadNoFocusLibrary = AlwaysLoadNoFocusLibrary;

            // pipe: step #4
            firefoxProfile.AssumeUntrustedCertificateIssuer = AssumeUntrustedCertificateIssuer;

            // pipe: step #5
            firefoxProfile.DeleteAfterUse = DeleteAfterUse;

            // pipe: step #6
            firefoxProfile.EnableNativeEvents = EnableNativeEvents;

            // pipe: step #7
            firefoxProfile.Port = Port;

            return firefoxProfile;
        }

        private FirefoxProfile ResetProfile()
        {
            // exit conditions
            if (string.IsNullOrEmpty(ProfileDirectory)) return new FirefoxProfile();

            // arguments to pass
            var args = new object[] { ProfileDirectory, DeleteSourceOnClean };

            // create profile instance
            return Activator.CreateInstance(typeof(FirefoxProfile), args) as FirefoxProfile;
        }
    }
}