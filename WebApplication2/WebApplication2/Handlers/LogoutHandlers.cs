using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Handlers
{
    public static class LogoutHandlers
    {
        public static async Task DoLogoutAsyn(HttpContext ctx)
        {
            ctx.Session.Clear();
            await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ctx.Response.Redirect("/login");
        }
    }
}
