using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models.Entities;

namespace WebApplication4.Models.Services
{
    public interface IBlogServices
    {
        IEnumerable<Post> GetLatestPosts(int max);
        IEnumerable<Post> GetPostsByDate(int year, int month);
        Post GetPost(string slug);
    }
}
