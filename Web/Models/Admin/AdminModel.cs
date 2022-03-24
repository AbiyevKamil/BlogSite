using System.Collections.Generic;
using Core.Entities;

namespace Web.Models.Admin
{
    public class AdminModel
    {
        // public User User { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}