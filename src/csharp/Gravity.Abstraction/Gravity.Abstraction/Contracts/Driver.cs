/*
 * CHANGE LOG
 * 
 * 2019-01-06
 *    - modify: change all types to string
 *    - modify: add XML comments
 *    
 * 2019-01-10
 *    - modify: moved to Gravity.Services.DataContracts name-space
 */
using System.Runtime.Serialization;

namespace Gravity.Abstraction.Contracts
{
    /// <summary>
    /// describes a contract for sending gravity driver-type to gravity-service
    /// </summary>
    [DataContract]
    public static class Driver
    {
        [DataMember]
        public const string Chrome = "ChromeDriver";

        [DataMember]
        public const string InternetExplorer = "IEDriverServer";

        [DataMember]
        public const string Firefox = "FirefoxDriver";

        [DataMember]
        public const string Edge = "MicrosoftWebDriver";

        [DataMember]
        public const string Mock = "MockWebDriver";

        [DataMember]
        public const string Remote = "RemoteWebDriver";

        [DataMember]
        public const string Appium = "AppiumDriver";

        [DataMember]
        public const string Android = "AndroidDriver";

        [DataMember]
        public const string iOS = "iOSDriver";

        [DataMember]
        public const string Base = "BaseDriver";

        [DataMember]
        public const string Safari = "SafariDriver";
    }
}