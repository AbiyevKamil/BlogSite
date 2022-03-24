using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Service.Business.Abstract
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task UpdateAsync(int id, Category category);
        Task<Category> FindByNameAsync(string categoryName);

    }
}