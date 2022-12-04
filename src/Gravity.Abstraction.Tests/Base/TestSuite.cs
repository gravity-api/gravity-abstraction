using Gravity.Abstraction.Contracts;
using Gravity.Abstraction.WebDriver;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Gravity.Abstraction.Tests.Base
{
    public abstract class TestSuite
    {
        protected TestSuite()
        { }

        /// <summary>
        /// Creates a <see cref="RemoteWebDriver"/> session using <see cref="DriverFactory"/>.
        /// </summary>
        /// <param name="onDriver"><see cref="RemoteWebDriver"/> to create based on <see cref="Driver"/>.</param>
        /// <param name="onTest">Test name under which this <see cref="RemoteWebDriver"/> was created.</param>
        /// <param name="onContext"><see cref="TestContext"/> under this run.</param>
        /// <returns><see cref="true"/> if <see cref="RemoteWebDriver"/> was created and dispose <see cref="false"/> if not.</returns>
        public static bool CreateRemoteDriver(string onDriver, string onTest, TestContext onContext)
        {
            // setup
            var driverParams = JsonSerializer.Serialize(new
            {
                Driver = onDriver,
                DriverBinaries = $"{onContext.Properties["Grid.Endpoint"]}",
                Options = new
                {
                    Application = "notepad.exe",
                    Arguments = new[] { "file.txt" }
                },
                Capabilities = new
                {
                    Project = onContext.Properties["Project.Name"],
                    Build = onContext.Properties["Build.Number"],
                    Name = onTest
                }
            }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            // execute
            var driver = new DriverFactory(driverParams).Create();

            // update context
            if(driver is IHasSessionId hasSession)
            {
                onContext.Properties["Driver.Session"] = hasSession.SessionId.ToString();
            }

            // assertion
            var actual = driver != null;
            driver.FindElement(By.Name("nameToFind"));

            // cleanup
            driver.Dispose();

            // result
            return actual;
        }

        /// <summary>
        /// Updates tests results on BrowserStack (if applicable)
        /// </summary>
        /// <param name="onContext"><see cref="TestContext"/> under this run.</param>
        public static void UpdateBrowserStack(TestContext onContext)
        {
            // setup
            var isBrowserStack = $"{onContext.Properties["Grid.Endpoint"]}".Contains("browserstack.com/wd/hub");

            // exit conditions
            if (!isBrowserStack)
            {
                return;
            }

            // request body
            var requestBody = new
            {
                Status = onContext.CurrentTestOutcome == UnitTestOutcome.Passed ? "passed" : "failed"
            };

            // update test outcome on 3rd party platform
            var requestUri = "https://api.browserstack.com/automate/sessions/<session-id>.json"
                .Replace("<session-id>", $"{onContext.Properties["Driver.Session"]}");
            Put(requestUri, requestBody, onContext);
        }

        private static void Put(string requestUri, object requestBody, TestContext onContext)
        {
            // setup
            var settings = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(scheme: "Basic", parameter: $"{onContext.Properties["Grid.BasicAuthorization"]}");

            // create content
            var body = JsonSerializer.Serialize(requestBody, settings);
            var stringContent = new StringContent(content: body, Encoding.UTF8, mediaType: "application/json");

            // send to server
            client.PutAsync(requestUri, stringContent).GetAwaiter().GetResult();
        }
    }
}