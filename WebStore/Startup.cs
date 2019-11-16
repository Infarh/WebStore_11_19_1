using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddMvc(
                opt =>
                {
                    //opt.Conventions.Add(new CustomControllerConvention());
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCookiePolicy();
            
            //app.UseAuthentication();
            //app.UseSession();

            //app.UseResponseCaching();
            //app.UseResponseCompression();

            //app.UseWelcomePage("/welcome"); //Пример промежуточного ПО.

            //app.Run(async context => await context.Response.WriteAsync("Hello World!")); // Безусловное выполнение (замыкает конвейер)

            app.Map("/Hello", application => application.Run(async ctx => await ctx.Response.WriteAsync("World!")));

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
