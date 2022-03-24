using System.Threading.Tasks;
using Core;
using Core.Entities;
using DataAccess.Repositories.Abstract.Base;

namespace DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> FindByNameAsync(string categoryName);
    }
}