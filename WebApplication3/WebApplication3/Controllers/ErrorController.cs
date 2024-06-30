﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace WebApplication3.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        public IActionResult Show(int id)
        {
            var html = string.Empty;
            if (id == 500)
            {
                var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature.Error; 
                var exceptionName = exception.GetType().Name; 
                _logger.LogError($"Exception thrown '{exceptionName}: {exception.Message}'"); 
                html = $@" 
                <!DOCTYPE html> 
                <html> 
                <head> 
                    <meta charset='utf-8' /> 
                    <title>Server error</title> 
                    <link href='/styles/calculator.css' rel='stylesheet' /> 
                </head> 
                <body>
                    <h1> 
                        <span class='statusCode'>500</span> 
                        Server error 
                    </h1> 
                    <p>We have detected a server error {exceptionName}.</p> 
                    <p><a href='javascript:history.back()'>Go back</a>.</p> 
                </body>
                </html> 
            ";
            } 
            else if (id == 404) 
            {
                var statusCodeFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>(); 
                var path = statusCodeFeature.OriginalPath; _logger.LogError($"Error 404 for path '{path}'"); 
                html = $@" 
                <!DOCTYPE html> 
                <html> 
                <head> 
                    <meta charset='utf-8' /> 
                    <title>Not found</title> 
                    <link href='/styles/calculator.css' rel='stylesheet' /> 
                </head> 
                <body>
                    <h1> 
                        <span class='statusCode'>404</span> 
                        Not found 
                    </h1> 
                    <p>No content found at '{path}'.</p> 
                    <p><a href='javascript:history.back()'>Go back</a>.</p> 
                </body>
                </html> 
            ";
            } 
            return Content(html, contentType: "text/html"); 
        } 
    }
}
