using System;
using Autofac;
using Autofac.Dnx;
using BookPortal.Core.Logging;
using BookPortal.Responses.Domain;
using BookPortal.Responses.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace BookPortal.Responses
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", true); ;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<ResponseContext>(options =>
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ResponsesService>();

            builder.Populate(services);
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggingService(
                Configuration.Get("AppSettings:LoggingService"),
                Configuration.Get("AppSettings:ApplicationName"),
                LogLevel.Information);

            app.UseMvc();

            //Populates the BookContext sample data
            SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
