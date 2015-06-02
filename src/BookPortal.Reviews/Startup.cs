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

namespace BookPortal.Reviews
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new Configuration();
            configuration.AddConfigurationService("http://localhost:6004", "BookPortalReviews");
            configuration.AddJsonFile("config.json", optional: true);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
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

            services.AddScoped<ReviewsService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggingService(
                Configuration.Get("AppSettings:LoggingService"),
                Configuration.Get("AppSettings:ApplicationName"),
                LogLevel.Warning);

            app.UseMvc();

            // create mappings
            MapperInitialization.Initialize();

            //Populates the BookContext sample data
            SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
