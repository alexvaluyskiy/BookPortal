using System;
using BookPortal.Core.Configuration;
using BookPortal.Core.Logging;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Linq;
using Autofac;
using Autofac.Framework.DependencyInjection;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using BookPortal.Ratings.Domain;
using BookPortal.Ratings.Services;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Runtime;
using Swashbuckle.Swagger;
using Microsoft.Data.Entity;

namespace BookPortal.Ratings
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            string configServiceUrl = "http://aspnet5-bookportal-configuration.azurewebsites.net/";

            var configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath);
            configuration.AddConfigurationService(configServiceUrl, "Shared");
            configuration.AddConfigurationService(configServiceUrl, "BookPortalRatings");

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

            // TODO: untill swagger updates to beta6
            services.AddSwaggerBeta6(c =>
            {
                c.SwaggerGeneratorOptions.Schemes = new[] { "http", "https" };
                c.SwaggerGeneratorOptions.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "BookPortal Ratings API"
                });

                c.SchemaGeneratorOptions.DescribeAllEnumsAsStrings = true;
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<RatingsContext>(options =>
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<RatingsService>();

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

            app.UseSwagger();
            app.UseSwaggerUi();

            // TODO: until EF7 fix migrations
            var context = app.ApplicationServices.GetService<RatingsContext>();
            context.Database.EnsureCreated();
        }
    }
}
