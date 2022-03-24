using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Implementation.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;
        private readonly DbSet<Blog> _dbSet;

        public BlogRepository(BlogContext context)
        {
            _context = context;
            _dbSet = _context.Blogs;
        }

        public async Task<IEnumerable<Blog>> GetAllPublicAsync()
        {
            return await _dbSet.Where(i => i.IsApproved && !i.IsDeleted)
                .Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllByUserAsync(User user)
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.User == user)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetPublicByTitleAsync(string blogTitle)
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.Title.Contains(blogTitle))
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .OrderByDescending(i => i.CreatedDate)
                .ToListAsync();
        }

        public async Task SoftDeleteAsync(Blog blog)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == blog.Id);
            if (exist != null)
                exist.IsDeleted = true;
        }

        public async Task ViewedBlogAsync(Blog blog)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == blog.Id);
            if (exist != null)
                exist.ViewCount++;
        }

        public async Task<IEnumerable<Blog>> GetPopularBlogsAsync(int numberOfBlogs)
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.IsApproved && !i.IsDeleted)
                .OrderByDescending(i => i.ViewCount).Take(numberOfBlogs).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetByCategoryNameAsync(string categoryName)
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .Where(i => i.IsApproved && !i.IsDeleted && i.Category.Name == categoryName)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task RestoreAsync(Blog blog)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == blog.Id);
            if (exist != null)
                exist.IsDeleted = false;
        }

        public async Task<Blog> FindPublicByUrlAsync(string url)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.UniqueUrl == url && !i.IsDeleted && i.IsApproved);
        }

        public async Task<IEnumerable<Blog>> FindPublicByUserAsync(User user)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Category)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Where(i => i.User == user && !i.IsDeleted && i.IsApproved)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task CreateAsync(Blog blog)
        {
            await _dbSet.AddAsync(blog);
        }

        public async Task UpdateAsync(int id, Blog blog)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == id);
            if (exist is not null)
            {
                exist.Banner = blog.Banner;
                exist.CategoryId = blog.CategoryId;
                exist.UniqueUrl = exist.Title == blog.Title ? exist.UniqueUrl : UrlGenerator.Generate(blog.Title);
                exist.Content = blog.Content;
                exist.Title = blog.Title;
                exist.UpdatedDate = DateTime.UtcNow;
            }
        }

        public async Task<Blog> FindPublicByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted && i.IsApproved);
        }

        public async Task<Blog> FindPrivateByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id && !i.IsDeleted);
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task ApproveAsync(int id)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == id);
            if (exist is not null)
            {
                exist.IsApproved = true;
            }
        }

        public async Task<Blog> FindPrivateByUrlAsync(string url)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.UniqueUrl == url && !i.IsDeleted);
        }

        public async Task<IEnumerable<Blog>> FindPrivateByUserAsync(User user)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Category)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Where(i => i.User == user && !i.IsDeleted)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> FindDeletedByUserAsync(User user)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Category)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Where(i => i.User == user && i.IsDeleted)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> FindDeletedByIdAsync(int id)
        {
            return await _dbSet.Include(i => i.User).Include(i => i.Category)
                .Include(i => i.Comments)
                .Where(i => i.Id == id && i.IsDeleted)
                .OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task CreateCommentAsync(Blog blog, Comment comment)
        {
            var exist = await _dbSet.Include(i => i.Comments).FirstOrDefaultAsync(i => i.Id == blog.Id);
            if (exist != null)
            {
                exist.Comments.Add(comment);
            }
        }
    }
}