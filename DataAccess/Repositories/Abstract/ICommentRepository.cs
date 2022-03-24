using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Repositories.Abstract.Base;

namespace DataAccess.Repositories.Abstract
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllByUserAsync(User user);
    }
}