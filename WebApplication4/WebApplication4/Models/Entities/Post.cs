using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}
