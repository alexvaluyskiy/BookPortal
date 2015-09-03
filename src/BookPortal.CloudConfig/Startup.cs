using BookPortal.CloudConfig.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using BookPortal.Core.Framework;
using BookPortal.Core.Framework.Filters;
using Microsoft.Dnx.Runtime;

namespace BookPortal.CloudConfig
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new ConfigurationBuilder(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");

            Configuration = configuration.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                // setup json output serializer
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(JsonFormatterFactory.Create());

                // add filters
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<ConfigContext>(options =>
                    options.UseSqlServer(Configuration.GetSection("Data:DefaultConnection:ConnectionString").Value));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Verbose);

            app.UseMvc();
        }
    }
}
