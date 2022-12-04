using Newtonsoft.Json.Linq;

using OpenQA.Selenium.Remote;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        [SuppressMessage(
            "Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
            Justification = "Private filed value must be normalized to support UiaDriver")]
        protected override void OnSendingRemoteHttpRequest(SendingRemoteHttpRequestEventArgs eventArgs)
        {
            // setup
            var json = JObject.Parse(eventArgs.RequestBody);
            var filed = eventArgs.GetType().GetField("requestBody", BindingFlags.Instance | BindingFlags.NonPublic);

            // clean capabilities
            json.Remove("capabilities");
            filed.SetValue(eventArgs, $"{json}");

            // clear
            base.OnSendingRemoteHttpRequest(eventArgs);
        }
    }
}
