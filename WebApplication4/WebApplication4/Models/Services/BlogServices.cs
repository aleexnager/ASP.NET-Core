using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models.Entities;
using WebApplication4.Models.Services;

namespace WebApplication4.Models.Services
{
    public class BlogServices : IBlogServices
    {
        // Repositorio en memoria
        private static readonly List<Post> Posts;

        // Poblado inicial de datos
        static BlogServices()
        {
            Posts = new List<Post>
            {
                new Post() {Title = "Welcome to MVC", Slug = "welcome-to-mvc", Author = "anager", Text = "Hi! Welcome to MVC!", Date = new DateTime(2024, 06, 21)},
                new Post() {Title = "Second Post", Slug = "second-post", Author = "anager", Text = "Hello, this is my second post", Date = new DateTime(2024, 06, 22)},
                new Post() {Title = "Another Post", Slug = "another-post", Author = "anager", Text = "Wow, and another one", Date = new DateTime(2024, 06, 23)},
            };

            for (int i = 1; i < 5; i++)
            {
                Posts.Add(
                    new Post() { Title = $"Post number {i}", Slug = $"Post-number-{i}", Author = "anager", Text = $"Text of post #{i}", Date = new DateTime(2024, 06, 28).AddDays(i) }
                );
            }

            var rnd = new Random(); 
            foreach (var post in Posts) 
            { 
                for (int i = 0; i < rnd.Next(4); i++) 
                { 
                    post.Comments.Add(new Comment() 
                    { 
                        Author = $"user{rnd.Next(1000)}", 
                        Date = post.Date.AddDays(rnd.Next(0, 100)), 
                        Text = $"Hello, your post {post.Title} looks great!" 
                    }); 
                } 
            } //foreach
        }

        // Miembros de IBlogServices:
        public IEnumerable<Post> GetLatestPosts(int max)
        {
            var posts = from post in Posts orderby post.Date descending select post;
            return posts.Take(max).ToList();
        }

        public IEnumerable<Post> GetPostsByDate(int year, int month)
        {
            var posts = from post in Posts where post.Date.Month == month && post.Date.Year == year orderby post.Date descending select post;
            return posts.ToList();
        }

        public Post GetPost(string slug)
        {
            return Posts.FirstOrDefault(post =>
                post.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}
