using Core.Entities;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Implementation.Base;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BlogContext context) : base(context)
        {
        }
    }
}