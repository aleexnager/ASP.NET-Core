using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Services;

namespace WebApplication4.Controllers
{
    public class BlogController: Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        public IActionResult Index()
        {
            var posts = _blogServices.GetLatestPosts(10);   // Se obtiene info
            //return View("Index", posts);                  // Seleccion de vista y envio
            return View(posts);                             // Se puede simplificar porque el nombre de la vista a retornar coincide con el de la acción actual
        }

        public IActionResult Archive(int year, int month)
        {
            var posts = _blogServices.GetPostsByDate(year, month);
            return View(posts);
        }

        [Route("view-post/{slug}")]
        public IActionResult ViewPost(string slug)
        {
            var post = _blogServices.GetPost(slug);
            if (post == null)
                return NotFound();
            else
                return View(post);
        }

        /*public IActionResult Save(Product product)
        {

        }*/


    }
}
