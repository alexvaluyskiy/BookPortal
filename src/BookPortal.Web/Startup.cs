using System;
using System.Linq;
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
using Newtonsoft.Json;

namespace BookPortal.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new Configuration();
            configuration.AddConfigurationService("http://localhost:6004", "BookPortalWeb");
            configuration.AddJsonFile("config.json", optional: true);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
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

            services.AddScoped<AwardsService>();
            services.AddScoped<NominationsService>();
            services.AddScoped<ContestsService>();
            services.AddScoped<ContestsWorksService>();

            services.AddScoped<PersonsService>();
            services.AddScoped<WorksService>();
            services.AddScoped<TranslationsService>();
            services.AddScoped<EditionsService>();

            services.AddScoped<PublishersService>();
            services.AddScoped<SeriesService>();

            services.AddScoped<CountriesService>();
            services.AddScoped<LanguagesService>();

            services.AddScoped<ImportersService>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLoggingService(
                Configuration.Get("AppSettings:LoggingService"),
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
