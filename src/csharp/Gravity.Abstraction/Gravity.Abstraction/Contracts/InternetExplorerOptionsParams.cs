using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenQA.Selenium.IE;
using System;
using System.Runtime.Serialization;
using Gravity.Abstraction.Interfaces;

namespace Gravity.Abstraction.Contracts
{
    [DataContract]
    public class InternetExplorerOptionsParams : DriverOptionsParams, IOptionable<InternetExplorerOptions>
    {
        /// <summary>
        /// Gets or sets the amount of time the driver will attempt to look for a newly launched
        /// instance of Internet Explorer.
        /// </summary>
        /// <value>
        /// The browser attach timeout.
        /// </value>
        [DataMember]
        public TimeSpan BrowserAttachTimeout { get; set; }

        /// <summary>
        /// Gets or sets the command line arguments used in launching Internet Explorer when
        /// the Windows CreateProcess API is used. This property only has an effect when
        /// the OpenQA.Selenium.IE.InternetExplorerOptions.ForceCreateProcessApi is true.
        /// </summary>
        /// <value>
        /// The browser command line arguments.
        /// </value>
        [DataMember]
        public string BrowserCommandLineArguments { get; set; }

        /// <summary>
        /// Gets or sets the value for describing how elements are scrolled into view in
        /// the IE driver. Defaults to scrolling the element to the top of the view-port.
        /// </summary>
        /// <value>
        /// The element scroll behavior.
        /// </value>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public InternetExplorerElementScrollBehavior ElementScrollBehavior { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use native events in interacting with
        /// elements.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable native events]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool EnableNativeEvents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable persistently sending WM_MOUSEMOVE
        /// messages to the IE window during a mouse hover.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable persistent hover]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool EnablePersistentHover { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to clear the Internet Explorer cache
        /// before launching the browser. When set to true, clears the system cache for all
        /// instances of Internet Explorer, even those already running when the driven instance
        /// is launched. Defaults to false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ensure clean session]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool EnsureCleanSession { get; set; }

        /// <summary>
        /// Gets or sets the amount of time the driver will attempt to look for the file
        /// selection dialog when attempting to upload a file.
        /// </summary>
        /// <value>
        /// The file upload dialog timeout.
        /// </value>
        [DataMember]
        public TimeSpan FileUploadDialogTimeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to force the use of the Windows CreateProcess
        /// API when launching Internet Explorer. The default value is false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force create process API]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool ForceCreateProcessApi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to force the use of the Windows ShellWindows
        /// API when attaching to Internet Explorer. The default value is false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force shell windows API]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool ForceShellWindowsApi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore the zoom level of Internet
        /// Explorer.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore zoom level]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IgnoreZoomLevel { get; set; }

        /// <summary>
        /// Gets or sets the initial URL displayed when IE is launched. If not set, the browser
        /// launches with the internal startup page for the WebDriver server.
        /// </summary>
        /// <value>
        /// The initial browser URL.
        /// </value>
        /// <remarks>
        /// By setting the OpenQA.Selenium.IE.InternetExplorerOptions.IntroduceInstabilityByIgnoringProtectedModeSettings
        /// to true and this property to a correct URL, you can launch IE in the Internet
        /// Protected Mode zone. This can be helpful to avoid the flakiness introduced by
        /// ignoring the Protected Mode settings. Nevertheless, setting Protected Mode zone
        /// settings to the same value in the IE configuration is the preferred method.
        /// </remarks>
        [DataMember]
        public string InitialBrowserUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore the settings of the Internet
        /// Explorer Protected Mode.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [introduce instability by ignoring protected mode settings]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool IntroduceInstabilityByIgnoringProtectedModeSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to require the browser window to have
        /// focus before interacting with elements.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [require window focus]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool RequireWindowFocus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use the supplied OpenQA.Selenium.IE.InternetExplorerOptions.Proxy
        /// settings on a per-process basis, not updating the system installed proxy setting.
        /// This property is only valid when setting a OpenQA.Selenium.IE.InternetExplorerOptions.Proxy,
        /// where the OpenQA.Selenium.Proxy.Kind property is either OpenQA.Selenium.ProxyKind.Direct,
        /// OpenQA.Selenium.ProxyKind.System, or OpenQA.Selenium.ProxyKind.Manual, and is
        /// otherwise ignored. Defaults to false.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use per process proxy]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool UsePerProcessProxy { get; set; }

        /// <summary>
        /// Generate DriverOptions for the current driver based of the params object
        /// </summary>
        /// <returns>
        /// DriverOptions
        /// </returns>
        public InternetExplorerOptions ToDriverOptions()
        {
            var ieOptions = new InternetExplorerOptions();                       // initialize response

            ToDriverOptions(ieOptions);                                          // pipe: step #1
            ieOptions.BrowserAttachTimeout = BrowserAttachTimeout;               // pipe: step #2
            ieOptions.BrowserCommandLineArguments = BrowserCommandLineArguments; // pipe: step #3
            ieOptions.ElementScrollBehavior = ElementScrollBehavior;             // pipe: step #4
            ieOptions.EnableNativeEvents = EnableNativeEvents;                   // pipe: step #5
            ieOptions.EnablePersistentHover = EnablePersistentHover;             // pipe: step #6
            ieOptions.EnsureCleanSession = EnsureCleanSession;                   // pipe: step #7
            ieOptions.FileUploadDialogTimeout = FileUploadDialogTimeout;         // pipe: step #8
            ieOptions.ForceCreateProcessApi = ForceCreateProcessApi;             // pipe: step #9
            ieOptions.ForceShellWindowsApi = ForceShellWindowsApi;               // pipe: step #10
            ieOptions.IgnoreZoomLevel = IgnoreZoomLevel;                         // pipe: step #11
            ieOptions.InitialBrowserUrl = InitialBrowserUrl;                     // pipe: step #12
            ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = IntroduceInstabilityByIgnoringProtectedModeSettings;
            ieOptions.RequireWindowFocus = RequireWindowFocus;                   // pipe: step #14
            ieOptions.UsePerProcessProxy = UsePerProcessProxy;                   // pipe: step #15

            return ieOptions; // complete pipe
        }
    }
}