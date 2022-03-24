using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.UnitOfWork.Abstract;
using Service.Business.Abstract;

namespace Service.Business.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task CreateAsync(Category category)
        {
             await _unitOfWork.Categories.CreateAsync(category);
             await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(int id, Category category)
        {
            await _unitOfWork.Categories.UpdateAsync(id,category);
            await _unitOfWork.CommitAsync();
        }
    }
}