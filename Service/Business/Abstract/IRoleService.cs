using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Service.Business.Abstract
{
    public interface IRoleService
    {
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult>  DeleteAsync(Role role);
        Task<IdentityResult>  UpdateAsync(string id, Role role);
        Task<bool> ExistsAsync(string roleName);
        Task<Role> FindByIdAsync(string id);
        Task<Role> FindByName(string roleName);
    }
}