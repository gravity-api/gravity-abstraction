using Newtonsoft.Json.Linq;

using OpenQA.Selenium.Remote;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Gravity.Abstraction.Uia
{
    public class UiaCommandExecutor : HttpCommandExecutor
    {
        [SuppressMessage("Roslynator", "RCS1169:Make field read-only.", Justification = "Shadow field from base class")]
        private Uri remoteServerUri;

        public UiaCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout)
            : base(addressOfRemoteServer, timeout)
        {
            remoteServerUri = SetRemoteServerUri();
        }

        public UiaCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout, bool enableKeepAlive)
            : base(addressOfRemoteServer, timeout, enableKeepAlive)
        {
            remoteServerUri = SetRemoteServerUri();
        }

        public Uri RemoteServerUri { get => remoteServerUri; }

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

        [SuppressMessage(
            "Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields",
            Justification = "Need access to the server Uri for custom commands")]
        private Uri SetRemoteServerUri()
        {
            // constants
            const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

            // get
            return GetType().BaseType.GetField("remoteServerUri", Flags).GetValue(this) as Uri;
        }
    }
}
