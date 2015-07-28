using System;
using BookPortal.Core.Configuration;
using BookPortal.Core.Logging;
using BookPortal.Reviews.Domain;
using BookPortal.Reviews.Infrastructure;
using BookPortal.Reviews.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Linq;
using Autofac;
using Autofac.Framework.DependencyInjection;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using Microsoft.Framework.Runtime;

namespace BookPortal.Reviews
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");
            configuration.AddConfigurationService("http://localhost:6004", "Shared");
            configuration.AddConfigurationService("http://localhost:6004", "BookPortalReviews");

            Configuration = configuration.Build();
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

            loggerFactory.AddDebug(LogLevel.Verbose);

            app.UseMvc();

            // create mappings
            MapperInitialization.Initialize();

            //Populates the BookContext sample data
            //SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
