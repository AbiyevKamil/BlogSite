using System.Threading.Tasks;
using Core.Entities;
using DataAccess.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Identity;
using Service.Business.Abstract;

namespace Service.Business.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> CreateAsync(Role role)
        {
            return await _unitOfWork.Roles.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(Role role)
        {
            return await _unitOfWork.Roles.DeleteAsync(role);
        }

        public async Task<IdentityResult> UpdateAsync(string id, Role role)
        {
            return await _unitOfWork.Roles.UpdateAsync(id, role);
        }

        public async Task<bool> ExistsAsync(string roleName)
        {
            return await _unitOfWork.Roles.ExistsAsync(roleName);
        }

        public async Task<Role> FindByIdAsync(string id)
        {
            return await _unitOfWork.Roles.FindByIdAsync(id);
        }

        public async Task<Role> FindByName(string roleName)
        {
            return await _unitOfWork.Roles.FindByName(roleName);
        }
    }
}