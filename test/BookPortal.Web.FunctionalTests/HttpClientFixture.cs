using System;
using System.Net.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace BookPortal.Web.FunctionalTests
{
    public class HttpClientFixture : IDisposable
    {
        private readonly TestServer _testServer;

        public HttpClientFixture()
        {
            var appEnv = CallContextServiceLocator.Locator.ServiceProvider.GetRequiredService<IApplicationEnvironment>();
            var hostEnv = new HostingEnvironment { EnvironmentName = "Test" };
            var startup = new Startup(hostEnv, appEnv);
            _testServer = TestServer.Create(null, a => startup.Configure(a, new LoggerFactory()), s => startup.ConfigureServices(s));
            HttpClient = _testServer.CreateClient();
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _testServer.Dispose();
        }

        public HttpClient HttpClient { get; private set; }
    }
}
