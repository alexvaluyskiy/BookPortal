using BookPortal.Logging.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Logging
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", true); ;

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

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            app.ApplicationServices.GetRequiredService<LogsContext>().Database.EnsureCreated();
        }
    }
}
