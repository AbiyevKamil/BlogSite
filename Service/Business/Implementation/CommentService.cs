using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.UnitOfWork.Abstract;
using Service.Business.Abstract;

namespace Service.Business.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _unitOfWork.Comments.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _unitOfWork.Comments.GetAllAsync();
        }

        public async Task CreateAsync(Comment comment)
        {
            await _unitOfWork.Comments.CreateAsync(comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            await _unitOfWork.Comments.DeleteAsync(comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, Comment comment)
        {
            await _unitOfWork.Comments.UpdateAsync(id, comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByUserAsync(User user)
        {
            return await _unitOfWork.Comments.GetAllByUserAsync(user);
        }
    }
}