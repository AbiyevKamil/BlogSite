using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Implementation;
using DataAccess.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BlogContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger _logger;

        public UnitOfWork(BlogContext context,
            UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

            Users = new UserRepository(_context, _userManager);
            Roles = new RoleRepository(_context, _roleManager);
            Blogs = new BlogRepository(_context);
            Comments = new CommentRepository(_context);
            Categories = new CategoryRepository(_context);
        }

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IBlogRepository Blogs { get; }
        public ICommentRepository Comments { get; }
        public ICategoryRepository Categories { get; }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}