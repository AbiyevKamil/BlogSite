using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Abstract.Base;
using DataAccess.Repositories.Implementation.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext context) : base(context)
        {
        }

        public override async Task DeleteAsync(Comment comment)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(i => i.Id == comment.Id);
            if (exist is not null)
                _dbSet.Remove(exist);
        }

        public async Task<IEnumerable<Comment>> GetAllByUserAsync(User user)
        {
            return await _dbSet.Include(i => i.User)
                .Include(i => i.Blog)
                .Where(i => i.User == user)
                .ToListAsync();
        }
    }
}