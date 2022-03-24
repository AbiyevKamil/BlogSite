using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Implementation.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Category entity)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == entity.Id);
            if (exist is not null)
            {
                _dbSet.Remove(exist);
            }
        }

        public async Task<Category> FindByNameAsync(string categoryName)
        {
            return await _dbSet.Include(i => i.Blogs)
                .FirstOrDefaultAsync(i => i.Name == categoryName);
        }
    }
}