using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Repositories.Abstract.Base;

namespace DataAccess.Repositories.Abstract
{
    public interface IBlogRepository
    {
        Task<Blog> FindPublicByUrlAsync(string url);
        Task<IEnumerable<Blog>> FindPublicByUserAsync(User user);
        Task CreateAsync(Blog blog);
        Task UpdateAsync(int id, Blog blog);
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<IEnumerable<Blog>> GetAllByUserAsync(User user);
        Task<IEnumerable<Blog>> GetPublicByTitleAsync(string blogTitle);

        Task<Blog> FindPublicByIdAsync(int id);
        Task<Blog> FindPrivateByIdAsync(int id);

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