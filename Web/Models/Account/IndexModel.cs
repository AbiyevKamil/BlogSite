using System.Collections.Generic;
using Core.Entities;

namespace Web.Models.Account
{
    public class IndexModel
    {
        public User User { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}