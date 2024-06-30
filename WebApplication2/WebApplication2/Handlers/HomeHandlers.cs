using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApplication2.Handlers
{
    public static class HomeHandlers
    {
        public static async Task GetHomePageAsync(HttpContext ctx)
        {
            await ctx.Session.LoadAsync();
            var cache = ctx.RequestServices.GetService<IDistributedCache>();

            // Contador de la sesion
            var visitCount = ctx.Session.GetInt32("VisitCount").GetValueOrDefault() + 1;
            ctx.Session.SetInt32("VisitCount", visitCount);

            // Contador de la cache
            string cacheKey = $"{ctx.User.Identity.Name}_VisitCount";
            string cachedVisitCount = await cache.GetStringAsync(cacheKey);
            int cachedCount = string.IsNullOrEmpty(cachedVisitCount) ? 0 : int.Parse(cachedVisitCount);
            cachedCount++;
            await cache.SetStringAsync(cacheKey, cachedCount.ToString());
            var body = $@"
                    <h1>Home</h1> 
                    <p>Hello, {ctx.User.Identity.Name}!</p>
                    <p>Your visits (session): {cachedCount}      Total visits (cached): {visitCount}</p>
                    <p><a href='/logout'>Logout</a></p>";
            await PageUtils.SendPageAsync(ctx, "Home", body);
            
        }
    }
}