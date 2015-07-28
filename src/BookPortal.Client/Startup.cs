using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;

namespace BookPortal.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseFileServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute("",
                    "{controller}/{action}", 
                    new { controller = "Home", action = "Index" });
            });
        }
    }
}
