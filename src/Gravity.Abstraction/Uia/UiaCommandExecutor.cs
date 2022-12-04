using Newtonsoft.Json.Linq;

using OpenQA.Selenium.Remote;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gravity.Abstraction.Uia
{
    public class UiaCommandExecutor : HttpCommandExecutor
    {
        public UiaCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout)
            : base(addressOfRemoteServer, timeout)
        { }

        public UiaCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout, bool enableKeepAlive)
            : base(addressOfRemoteServer, timeout, enableKeepAlive)
        { }

        protected override void OnSendingRemoteHttpRequest(SendingRemoteHttpRequestEventArgs eventArgs)
        {
            // setup
            var json = JObject.Parse(eventArgs.RequestBody);
            var filed = eventArgs.GetType().GetField("requestBody", BindingFlags.Instance | BindingFlags.NonPublic);
            
            // clean capabilites
            json.Remove("capabilities");
            filed.SetValue(eventArgs, $"{json}");

            // clear
            base.OnSendingRemoteHttpRequest(eventArgs);
        }
    }
}
