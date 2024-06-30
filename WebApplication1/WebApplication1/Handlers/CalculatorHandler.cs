using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApplication1.Services;
using Microsoft.Extensions.DependencyInjection;


namespace WebApplication1.Handlers
{
    public class CalculatorHandler
    {
        public static async Task CalculateAsync(HttpContext ctx)
        {
            var a = Convert.ToInt32(ctx.GetRouteValue("a"));
            var b = Convert.ToInt32(ctx.GetRouteValue("b"));
            var operation = ctx.GetRouteValue("operation").ToString();
            var services = ctx.RequestServices.GetService<ICalculatorServices>();
            var result = services.Calculate(a,b,operation);
            await ctx.Response.WriteAsync(result.ToString());
        }
    }
}
