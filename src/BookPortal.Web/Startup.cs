﻿using System;
using System.Linq;
using Autofac;
using Autofac.Framework.DependencyInjection;
using BookPortal.Core.Configuration;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using BookPortal.Core.Logging;
using BookPortal.Web.Domain;
using BookPortal.Web.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Caching.Redis;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using Swashbuckle.Swagger;

namespace BookPortal.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            string configServiceUrl = "http://aspnet5-bookportal-configuration.azurewebsites.net/";

            var configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");
            configuration.AddConfigurationService(configServiceUrl, "Shared");
            configuration.AddConfigurationService(configServiceUrl, "BookPortalWeb");

            Configuration = configuration.Build();
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc().Configure<MvcOptions>(options =>
            {
                // setup json output serializer
                var formatter = options.OutputFormatters
                    .SingleOrDefault(c => c.GetType() == typeof (JsonOutputFormatter));
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
                    Title = "BookPortal Main API"
                });

                c.SchemaGeneratorOptions.DescribeAllEnumsAsStrings = true;
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<BookContext>(options => 
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

            services.AddApplicationInsightsTelemetry(Configuration);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<AwardsService>();
            builder.RegisterType<NominationsService>();
            builder.RegisterType<ContestsService>();
            builder.RegisterType<ContestsWorksService>();

            builder.RegisterType<PersonsService>();
            builder.RegisterType<WorksService>();
            builder.RegisterType<TranslationsService>();
            builder.RegisterType<EditionsService>();

            builder.RegisterType<PublishersService>();
            builder.RegisterType<SeriesService>();

            builder.RegisterType<CountriesService>();
            builder.RegisterType<LanguagesService>();

            builder.RegisterType<ReviewsService>();
            builder.RegisterType<GenresService>();
            builder.RegisterType<RatingsService>();

            builder.RegisterInstance(new RedisCache(new RedisCacheOptions
            {
                Configuration = "aspnet5-bookportal-cache.redis.cache.windows.net,ssl=true,password=+RoCREJvEK9gzZURHmFxNgMuF8tuhH2gKrr2omjrc7w="
            }));

            builder.Populate(services);
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Verbose);

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseErrorHandler(builder => builder.Run(ErrorRequestHandler.HandleErrorRequest));

            app.ApplicationServices.GetService<ILoggerFactory>();

            app.UseApplicationInsightsTracingTelemetry(LogLevel.Information);
            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
