using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Runtime.Infrastructure;
using BookPortal.Web;
using Microsoft.AspNet.TestHost;
using Xunit;

namespace BookPortal.Web.Tests.Integration
{
    public class AwardsControllerTest
    {
        private readonly Action<IApplicationBuilder> _app;
        private readonly IServiceProvider _services;
        private readonly HttpClient _client;

        public AwardsControllerTest()
        {
            var environment = CallContextServiceLocator.Locator.ServiceProvider
                .GetRequiredService<IApplicationEnvironment>();

            var startup = new Startup(new HostingEnvironment(environment));
            _app = startup.Configure;
            _services = startup.ConfigureServices(_app.);

            var server = TestServer.Create(_services, _app);
            _client = server.CreateClient();
        }

        [Fact]
        public async Task GetAllAwards_OK()
        {
            var response = await _client.GetAsync("api/awards");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
