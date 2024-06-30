using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using WebApplication2.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/login";
               });

            services.AddAuthorization();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/login", LoginHandlers.GetLoginPageAsync);
                endpoints.MapPost("/login", LoginHandlers.DoLoginAsyn);
                endpoints.MapGet("/home", HomeHandlers.GetHomePageAsync).RequireAuthorization();
                endpoints.MapGet("/logout", LogoutHandlers.DoLogoutAsyn);
            });

            app.Run(async ctx =>
            {
                await ctx.Response.WriteAsync("Prueba a poner /login en el buscador");
            });
        }
    }
}
