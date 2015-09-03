using System;
using System.Reflection;
using Autofac;
using Autofac.Framework.DependencyInjection;
using BookPortal.Core.Configuration;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using BookPortal.Core.Logging;
using BookPortal.Web.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.Caching.Redis;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Swashbuckle.Swagger;

namespace BookPortal.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            string configServiceUrl = "http://aspnet5-bookportal-configuration.azurewebsites.net/";

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath);
            builder.AddJsonFile("config.json");
            builder.AddConfigurationService(configServiceUrl, "Shared");
            builder.AddConfigurationService(configServiceUrl, "BookPortalWeb");

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors();

            services.AddMvc(options =>
            {
                // setup json output serializer
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(JsonFormatterFactory.Create());

                // add filters
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddSwagger(c =>
            {
                c.SwaggerGeneratorOptions.Schemes = new[] { "http", "https" };
                c.SwaggerGeneratorOptions.BasePath = "/";
                c.SwaggerGeneratorOptions.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "BookPortal Main API",
                    Description = "This is a basic BookPortal API. The Api still in active development."
                });

                c.SchemaGeneratorOptions.DescribeAllEnumsAsStrings = true;
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<BookContext>(options => 
                    options.UseSqlServer(Configuration.GetSection("Data:DefaultConnection:ConnectionString").Value));

            services.AddApplicationInsightsTelemetry(Configuration);

            ContainerBuilder builder = new ContainerBuilder();

            var dataAccess = Assembly.GetExecutingAssembly();

            // register services
            builder.RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("Service"))
               .InstancePerLifetimeScope();

            // register repositories
            builder.RegisterAssemblyTypes(dataAccess)
               .Where(t => t.Name.EndsWith("Repository"))
               .InstancePerLifetimeScope();

            builder.RegisterType<ConnectionFactory>()
                .As<IConnectionFactory>()
                .WithParameter("connectionString", Configuration.GetSection("Data:DefaultConnection:ConnectionString").Value)
                .InstancePerLifetimeScope();

            builder.RegisterInstance(new RedisCache(new RedisCacheOptions { Configuration = Configuration.GetSection("RedisCache").Value }));
            builder.RegisterType<MemoryCache>().As<IMemoryCache>();

            builder.Populate(services);
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Verbose);
            // Add Application Insights tracing
            loggerFactory.AddApplicationInsightsLoggingService(app, LogLevel.Information);

            // Add Application Insights to the request pipeline to track HTTP request telemetry data.
            app.UseApplicationInsightsRequestTelemetry();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseErrorHandler(builder => builder.Run(ErrorRequestHandler.HandleErrorRequest));

            // Track data about exceptions from the application. Should be configured after all error handling middleware in the request pipeline.
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            // Add swagger to the pipeline
            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
