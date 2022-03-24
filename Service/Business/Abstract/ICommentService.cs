using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Service.Business.Abstract
{
    public interface ICommentService
    {
        Task<Comment> GetByIdAsync(int id);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task CreateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task UpdateAsync(int id, Comment comment);
        Task<IEnumerable<Comment>> GetAllByUserAsync(User user);
    }
}