using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models.Entities;

namespace WebApplication7.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult Index() => Content("FriendsController.Index()");

        public IActionResult Create()
        {
            var newFriend = new Friend() { Name = "Your Name", Age = 21, Email = "example@example.com", Address = new Address() };
            return View(newFriend);
        }

        [HttpPost]
        public IActionResult Create(Friend friend)
        {
            if (!ModelState.IsValid)
                return View(friend);

            return Content($"Created: {friend.Name}");
        }

        [HttpPost]
        public IActionResult Edit(Friend friend)
        {
            if (!ModelState.IsValid)
                return View(friend);
            else
                return View();
        }
    }
}
