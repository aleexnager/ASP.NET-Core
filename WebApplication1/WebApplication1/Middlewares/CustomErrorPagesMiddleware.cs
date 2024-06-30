using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApplication1.Middlewares
{
    public class CustomErrorPagesMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomErrorPagesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            if (ctx.Request.Path == "/error/show/500")
            {
                var exceptionHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                var exceptionName = exceptionHandlerFeature.Error.GetType().Name;
                await SendHtmlPage(ctx, statusCode: 500, title: "Server error", description: $"We have detected a server error {exceptionName}");
            }
            else if (ctx.Request.Path == "/error/show/404")
            {
                var statusCodeFeature = ctx.Features.Get<IStatusCodeReExecuteFeature>();
                var path = statusCodeFeature.OriginalPath;
                await SendHtmlPage(ctx, statusCode: 404, title: "Not found", description: $"No content found at '{path}'");
            }
            else
            {
                await _next(ctx);
            }
        }

        private async Task SendHtmlPage(HttpContext context, int statusCode, string title, string description)
        {
            var content = $@" 
                <!DOCTYPE html> 
                <html> 
                <head> 
                    <meta charset='utf-8' /> 
                    <title>{title}</title> 
                    <link href='/styles/calculator.css' rel='stylesheet' /> 
                </head> 
                <body>
                    <h1> 
                        <span class='statusCode'>{statusCode}</span> 
                        {title} 
                    </h1> 
                    <p>{description}.</p> 
                    <p><a href='javascript:history.back()'>Go back</a>.</p> 
                </body>
                </html> 
            "; 
            context.Response.Clear(); 
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "text/html"; 
            await context.Response.WriteAsync(content);
        }
    }
}
