using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Handlers
{
    public static class LoginHandlers
    {
        public static async Task GetLoginPageAsync(HttpContext ctx)
        {
            var body = @"
                <h1> Login </h1> 
                <form method='post' action='/login'> 
                    Username: <input type='text' name='username'><br> 
                    Password: <input type='password' name='password'><br> 
                    <hr> 
                    <input type='submit' value='Login'> 
                </form>
            "; 
            await PageUtils.SendPageAsync(ctx, "Login", body);
        }

        public static async Task DoLoginAsyn(HttpContext ctx)
        {
            /*
             * Obtener los datos del formulario y comprobar credenciales
             * a) Si las credenciales son incorrectas, mostramos una página de error
             * b) Si las credenciales son correctas, establecemos la cookie y redirigimos a /home
             * (Por simplificar) Podemos considerar correctas las credenciales cuando usr == pswrd
            */
            var username = ctx.Request.Form["username"].ToString();
            var password = ctx.Request.Form["password"].ToString();

            if (username == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                
                await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                ctx.Response.Redirect("/home");
            }
            else
            {
                await PageUtils.SendPageAsync(ctx, "Login Error", "<h1>Invalid credentials</h1>");
            }
        }
    }

    public static class PageUtils
    {
        public static async Task SendPageAsync(HttpContext ctx, string title, string body)
        {
            var content = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset='utf-8' /> 
                        <title>{title}</title> 
                        <link href='/styles/site.css' rel='stylesheet' /> 
                    </head> 
                    <body> 
                        {body} 
                    </body> 
                    </html> 
                ";
            ctx.Response.ContentType = "text/html";
            await ctx.Response.WriteAsync(content);
        }
    }
}
