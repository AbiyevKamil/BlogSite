using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Service.Business.Abstract
{
    public interface IBlogService
    {
        Task CreateAsync(Blog blog);
        Task UpdateAsync(int id, Blog blog);
        Task<IEnumerable<Blog>> GetAllPublicAsync();
        Task<Blog> FindPublicByUrlAsync(string url);
        Task<IEnumerable<Blog>> FindPublicByUserAsync(User user);
        Task<IEnumerable<Blog>> GetAllByUserAsync(User user);
        Task<IEnumerable<Blog>> GetPublicByTitleAsync(string blogTitle);
        public Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> FindPublicByIdAsync(int id);
        Task<Blog> FindPrivateByIdAsync(int id);
        Task<Blog> GetByIdAsync(int id);
        Task ApproveAsync(int id);

        Task<Blog> FindPrivateByUrlAsync(string url);
        Task<IEnumerable<Blog>> FindPrivateByUserAsync(User user);
        Task<IEnumerable<Blog>> FindDeletedByUserAsync(User user);
        Task<IEnumerable<Blog>> FindDeletedByIdAsync(int id);
        Task CreateCommentAsync(Blog blog, Comment comment);
        Task RestoreAsync(Blog blog);
        Task SoftDeleteAsync(Blog blog);
        Task ViewedBlogAsync(Blog blog);
        Task<IEnumerable<Blog>> GetPopularBlogsAsync(int numberOfBlogs);
        Task<IEnumerable<Blog>> GetByCategoryNameAsync(string categoryName);
    }
}