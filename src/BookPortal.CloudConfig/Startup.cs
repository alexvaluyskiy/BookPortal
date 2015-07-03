﻿using BookPortal.CloudConfig.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Linq;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using Microsoft.Framework.Runtime;

namespace BookPortal.CloudConfig
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new Configuration(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");

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
                options.OutputFormatters.Add(JsonFormatterFactory.Create());

                // add filters
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<ConfigContext>(options =>
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseMvc();

            //Populates the BookContext sample data
            //SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
