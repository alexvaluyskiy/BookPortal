using System;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Core.ApiPrimitives.Filters;
using BookPortal.Core.Configuration;
using BookPortal.Core.Logging;
using BookPortal.Reviews.Domain;
using BookPortal.Reviews.Infrastructure;
using BookPortal.Reviews.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Newtonsoft.Json;
using System.Linq;
using Autofac;
using Autofac.Dnx;
using Microsoft.Framework.Runtime;

namespace BookPortal.Reviews
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new Configuration(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "Shared");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "BookPortalReviews");

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options =>
            {
                // setup json output serializer
                var formatter = options.OutputFormatters
                    .SingleOrDefault(c => c.GetType() == typeof(JsonOutputFormatter));
                options.OutputFormatters.Remove(formatter);
                options.OutputFormatters.Add(JsonFormatterFactory.Create());

                // add filters
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<ReviewContext>(options =>
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ReviewsService>();

            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggingService(
                Configuration.Get("Services:LoggingService"),
                Configuration.Get("AppSettings:ApplicationName"),
                LogLevel.Warning);

            app.UseMvc();

            // create mappings
            MapperInitialization.Initialize();

            //Populates the BookContext sample data
            //SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
