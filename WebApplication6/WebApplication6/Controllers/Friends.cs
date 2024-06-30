using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace WebApplication5.Controllers
{
    [Route("Friends")]
    public class Friends : Controller
    {
        // GET: /Friends/
        [Route("")]
        public IActionResult Index() => Content("FriendsController.Index()");

        // GET: /Friends/View/John (restricción personalizada)
        [Route("View/{name:startsWith(Jo)}")]
        public IActionResult View(string name) => Content($"FriendsController.View('{name}')");

        // GET: /Friends/Edit/23
        [Route("Edit/{id}")]
        public IActionResult Edit(int id) => Content($"FriendsController.Edit('{id}')");

        // GET: /delete/friends/18 (num entero, > 0 y < 10000)
        [Route("/Delete/Friends/{id:int:min(1):max(9999)}")]
        public IActionResult Delete(int id) => Content($"FriendsController.Delete('{id}')");
    }
}
