﻿using System;
using System.Diagnostics;
using Autofac;
using Autofac.Dnx;
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
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true); ;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSubKey("AppSettings"));

            services.AddCors();

            services.AddMvc().Configure<MvcOptions>(options =>
            {
                // setup json output serializer
                var jsonOutputFormatter = new JsonOutputFormatter();
                jsonOutputFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonOutputFormatter.SerializerSettings.Formatting = Formatting.Indented;
                jsonOutputFormatter.SerializerSettings.ContractResolver = new LowerCasePropertyNamesContractResolver();

                options.OutputFormatters.RemoveTypesOf<JsonOutputFormatter>();
                options.OutputFormatters.Add(jsonOutputFormatter);

                // add filters
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<BookContext>(options => 
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));

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

        public void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Trace.AutoFlush = true;
            var sourceSwitch = new SourceSwitch("") { Level = SourceLevels.All };
            var traceListener = new TextWriterTraceListener(Configuration.Get("AppSettings:LogFilePath"));
            loggerFactory.AddTraceSource(sourceSwitch, traceListener);

            Configure(app, loggerFactory);
        }

        public void ConfigureProduction(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            Configure(app, loggerFactory);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseErrorHandler(builder => builder.Run(ErrorRequestHandler.HandleErrorRequest));

            app.UseMvc();

            // create mappings
            MapperInitialization.Initialize();

            //Populates the BookContext sample data
            SampleData.InitializeMusicStoreDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
