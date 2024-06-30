using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCalculator(this IApplicationBuilder app, string path)
        {
            return app.UseMiddleware<WebApplication1.Middlewares.CalculatorMiddleware>(path);
        }

        public static IApplicationBuilder UseCustomErrorPages(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebApplication1.Middlewares.CustomErrorPagesMiddleware>();
        }
    }
}
