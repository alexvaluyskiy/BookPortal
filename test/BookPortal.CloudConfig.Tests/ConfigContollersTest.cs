using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using BookPortal.CloudConfig.Controllers;
using BookPortal.CloudConfig.Domain;
using BookPortal.CloudConfig.Domain.Models;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc;
using System.Linq;

namespace BookPortal.CloudConfig.Test
{
    public class ConfigContollersTest
    {
        private readonly IServiceProvider _serviceProvider;

        public ConfigContollersTest()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                      .AddInMemoryStore()
                      .AddDbContext<ConfigContext>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task Get_Ok()
        {
            var dbContext = _serviceProvider.GetRequiredService<ConfigContext>();
            CreateSampleTask(dbContext);

            ConfigController controller = new ConfigController(dbContext);
            var actionResult = await controller.Get("Shared");
            var contentResult = actionResult as ObjectResult;
            
            Assert.NotNull(contentResult);
            Assert.IsType<List<KeyValuePair<string, string>>>(contentResult.Value);
            
            var content = (List<KeyValuePair<string, string>>)contentResult.Value;
            Assert.NotNull(content);
            Assert.Equal(2, content.Count);
            Assert.Equal("Database", content.Where(c => c.Key == "Database").Select(c => c.Key).SingleOrDefault());
            Assert.Equal("mssqldb", content.Where(c => c.Key == "Database").Select(c => c.Value).SingleOrDefault());
        }

        private void CreateSampleTask(ConfigContext dbContext)
        {
            var profile = new ConfigProfile() { Name = "Shared" };
            dbContext.Add(profile);
            dbContext.SaveChanges();
            
            var config1 = new Config() { Key = "Database", Value = "mssqldb", ProfileId = 1 };
            var config2 = new Config() { Key = "Service", Value = "http://localhost", ProfileId = 1 };
            dbContext.Add(config1);
            dbContext.Add(config2);
            dbContext.SaveChanges();
        }
    }
}
