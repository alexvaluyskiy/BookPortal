using System;
using System.Linq;
using Autofac;
using Autofac.Dnx;
using BookPortal.Core.ApiPrimitives;
using BookPortal.Core.ApiPrimitives.Filters;
using BookPortal.Core.Configuration;
using BookPortal.Core.Logging;
using BookPortal.Web.Domain;
using BookPortal.Web.Infrastructure;
using BookPortal.Web.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using Newtonsoft.Json;

namespace BookPortal.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new Configuration(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "Shared");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "BookPortalWeb");

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //IConfiguration configs = Configuration.GetSubKey("AppSettings");
            //services.Configure<AppSettings>(configs, 0, "Default");

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

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<BookContext>(options => 
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

            //services.AddApplicationInsightsTelemetry(Configuration);

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

            builder.RegisterType<ImportersService>();

            builder.Populate(services);
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggingService(
                Configuration.Get("Services:LoggingService"),
                Configuration.Get("AppSettings:ApplicationName"),
                LogLevel.Verbose);

            app.UseStaticFiles();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseErrorHandler(builder => builder.Run(ErrorRequestHandler.HandleErrorRequest));

            //app.UseApplicationInsightsRequestTelemetry();
            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            // create mappings
            MapperInitialization.Initialize();

            //Populates the BookContext sample data
            //SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
