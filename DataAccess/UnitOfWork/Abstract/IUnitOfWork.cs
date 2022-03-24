using System.Threading.Tasks;
using DataAccess.Repositories.Abstract;

namespace DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IBlogRepository Blogs { get; }
        public ICommentRepository Comments { get; }
        public ICategoryRepository Categories { get; }

        Task CommitAsync();
    }
}