using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Handlers;
using WebApplication1.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICalculatorServices, CalculatorServices>();
            services.AddTransient<ICalculationEngine, CalculationEngine>();
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            
            app.UseRouting();
            
            // Middleware para archivos predeterminados
            app.UseDefaultFiles(); // Busca archivos como index.html
            app.UseStaticFiles(); // Para archivos estaticos en wwwroot

            app.UseExceptionHandler("/error/show/500");
            app.UseStatusCodePagesWithReExecute("/error/show/{0}");
            app.UseCustomErrorPages();
            app.UseCalculator("/calc");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/calculator/{operation}/{a}/{b}", CalculatorHandler.CalculateAsync);
            });

            /*app.Run(async ctx =>
            {
                await ctx.Response.WriteAsync("Page Not Found");   
            });*/
        }
    }
}
