using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistance.Contexts
{
    public class BlogContext : IdentityDbContext<User, Role, string>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Blog

            builder.Entity<Blog>()
                .HasOne<User>(i => i.User)
                .WithMany(i => i.Blogs)
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            #endregion

            base.OnModelCreating(builder);
        }
    }
}