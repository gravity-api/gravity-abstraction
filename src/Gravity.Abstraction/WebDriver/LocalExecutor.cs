using OpenQA.Selenium.Remote;
using OpenQA.Selenium;

using System;

namespace Gravity.Abstraction.WebDriver
{
    public class LocalExecutor : HttpCommandExecutor
    {
        public LocalExecutor(string sessionId, Uri addressOfRemoteServer, TimeSpan timeout)
            : this(sessionId, addressOfRemoteServer, timeout, false)
        { }

        public LocalExecutor(string sessionId, Uri addressOfRemoteServer, TimeSpan timeout, bool enableKeepAlive)
            : this(addressOfRemoteServer, timeout, enableKeepAlive)
        {
            SessionId = sessionId;
        }

        public LocalExecutor(Uri addressOfRemoteServer, TimeSpan timeout, bool enableKeepAlive)
            : base(addressOfRemoteServer, timeout, enableKeepAlive)
        { }

        // expose the "Session ID" used to build this executor
        public string SessionId { get; }

        public override Response Execute(Command commandToExecute)
        {
            // invoke selenium command using the original driver executor
            if (!commandToExecute.Name.Equals("newSession"))
            {
                return base.Execute(commandToExecute);
            }

            // returns a success result, if the command is "newSession"
            // this prevents a shadow browser from opening
            return new Response
            {
                Status = WebDriverResult.Success,
                SessionId = SessionId,
                Value = null
            };
        }
    }
}
