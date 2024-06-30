using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    public class Friends : Controller
    {   
        // GET: /Friends/
        public IActionResult Index() => Content("FriendsController.Index()");

        // GET: /Friends/View/John
        public IActionResult View(string name) => Content($"FriendsController.View('{name}')");

        // GET: /Friends/Edit/23
        public IActionResult Edit(int id) => Content($"FriendsController.Edit('{id}')");

        // GET: /delete/friends/18
        public IActionResult Delete(int id) => Content($"FriendsController.Delete('{id}')");
    }
}
