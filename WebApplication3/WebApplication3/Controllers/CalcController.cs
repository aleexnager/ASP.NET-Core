using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;
using Microsoft.Extensions.Logging;

namespace WebApplication3.Controllers
{
    public class CalcController : Controller
    {
        private readonly ICalculatorServices _calculatorServices;
        private readonly ILogger _logger;

        public CalcController(ILogger<CalcController> logger, ICalculatorServices calculatorServices)
        {
            _logger = logger;
            _calculatorServices = calculatorServices;
        }

        public IActionResult Index()
        {
            var html = $@" 
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8' />
                    <title>Start - Calculator</title>
                    <link href='/styles/calculator.css' rel='stylesheet' />
                </head>
                <body>
                    <h1>
                        <img src='/images/calculator.png' />
                        MCV calculator 
                    </h1> 
                    <form method='post' action='calc/results'>
                        <input type='number' name='a'>
                        <select name='operation'>
                            <option value='+'>+</option>
                            <option value='-'>-</option>
                            <option value='*'>*</option>
                            <option value='/'>/</option>
                        </select> 
                        <input type='number' name='b'>
                        <input type='submit' value='Calculate'>
                    </form>
                </body> 
                </html>
            ";
            return Content(html, contentType: "text/html");
        }

        public IActionResult Results(int a, int b, string operation)
        {
            var result = _calculatorServices.Calculate(a, b, operation);
            var html = $@"<h2>{a}{operation}{b}={result}</h2>
                    <p><a href='/calc'>Back</a></p>
                ";
            return Content(html, contentType: "text/html");
        }
    }
}
