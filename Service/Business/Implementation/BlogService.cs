using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.UnitOfWork.Abstract;
using Service.Business.Abstract;

namespace Service.Business.Implementation
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Blog blog)
        {
            await _unitOfWork.Blogs.CreateAsync(blog);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, Blog blog)
        {
            await _unitOfWork.Blogs.UpdateAsync(id, blog);
            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _unitOfWork.Blogs.GetAllAsync();
        }

        public async Task<Blog> FindPublicByUrlAsync(string url)
        {
            return await _unitOfWork.Blogs.FindPublicByUrlAsync(url);
        }

        public async Task<IEnumerable<Blog>> FindPublicByUserAsync(User user)
        {
            return await _unitOfWork.Blogs.FindPublicByUserAsync(user);
        }

        public async Task<IEnumerable<Blog>> GetAllByUserAsync(User user)
        {
            return await _unitOfWork.Blogs.GetAllByUserAsync(user);
        }

        public async Task<IEnumerable<Blog>> GetPublicByTitleAsync(string blogTitle)
        {
            return await _unitOfWork.Blogs.GetPublicByTitleAsync(blogTitle);
        }

        public async Task<Blog> FindPublicByIdAsync(int id)
        {
            return await _unitOfWork.Blogs.FindPublicByIdAsync(id);
        }

        public async Task<Blog> FindPrivateByIdAsync(int id)
        {
            return await _unitOfWork.Blogs.FindPrivateByIdAsync(id);
        }

        public async Task<Blog> FindPrivateByUrlAsync(string url)
        {
            return await _unitOfWork.Blogs.FindPrivateByUrlAsync(url);
        }

        public async Task<IEnumerable<Blog>> FindPrivateByUserAsync(User user)
        {
            return await _unitOfWork.Blogs.FindPrivateByUserAsync(user);
        }

        public async Task<IEnumerable<Blog>> FindDeletedByUserAsync(User user)
        {
            return await _unitOfWork.Blogs.FindDeletedByUserAsync(user);
        }

        public async Task<IEnumerable<Blog>> FindDeletedByIdAsync(int id)
        {
            return await _unitOfWork.Blogs.FindDeletedByIdAsync(id);
        }

        public async Task CreateCommentAsync(Blog blog, Comment comment)
        {
            await _unitOfWork.Blogs.CreateCommentAsync(blog, comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task RestoreAsync(Blog blog)
        {
            await _unitOfWork.Blogs.RestoreAsync(blog);
            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteAsync(Blog blog)
        {
            await _unitOfWork.Blogs.SoftDeleteAsync(blog);
            await _unitOfWork.CommitAsync();
        }

        public async Task ViewedBlogAsync(Blog blog)
        {
            await _unitOfWork.Blogs.ViewedBlogAsync(blog);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Blog>> GetPopularBlogsAsync(int numberOfBlogs)
        {
            return await _unitOfWork.Blogs.GetPopularBlogsAsync(numberOfBlogs);
        }

        public async Task<IEnumerable<Blog>> GetByCategoryNameAsync(string categoryName)
        {
            return await _unitOfWork.Blogs.GetByCategoryNameAsync(categoryName);
        }
    }
}