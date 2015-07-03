using BookPortal.Core.Configuration;
using BookPortal.Core.Logging;
using BookPortal.Logging.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;

namespace BookPortal.Logging
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configuration = new Configuration(appEnv.ApplicationBasePath);
            configuration.AddJsonFile("config.json");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "Shared");
            configuration.AddConfigurationService(configuration.Get("Services:ConfigurationService"), "BookPortalLogging");

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
               .AddSqlServer()
               .AddDbContext<LogsContext>(options =>
                    options.UseSqlServer(Configuration.Get("Data:DefaultConnection:ConnectionString")));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new DebugLoggerProvider());

            app.UseMvc();

            // create sample data
            SampleData.InitializeDatabaseAsync(app.ApplicationServices).Wait();
        }
    }
}
