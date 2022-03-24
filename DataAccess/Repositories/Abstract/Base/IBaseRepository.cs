using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Base;

namespace DataAccess.Repositories.Abstract.Base
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(int id, T entity);
    }
}