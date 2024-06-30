using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Hello(string id)
        {
            var name = id ?? "user";
            return Content($"Hello from ASP.NET Core MVC, {name}!");
        }

        public IActionResult Index()
        {
            return Content($"What do you want to test?");
        }
    }
}
