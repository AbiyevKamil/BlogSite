using System.Threading.Tasks;
using Core.Entities;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BlogContext _context;
        private readonly ILogger _logger;
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(BlogContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateAsync(Role role) =>
            await _roleManager.CreateAsync(role);

        public async Task<IdentityResult> DeleteAsync(Role role) => 
            await _roleManager.DeleteAsync(role);

        public async Task<IdentityResult> UpdateAsync(string id, Role role) => 
            await _roleManager.UpdateAsync(role);

        public async Task<bool> ExistsAsync(string roleName) => 
            await _roleManager.RoleExistsAsync(roleName);

        public async Task<Role> FindByIdAsync(string id) => 
            await _roleManager.FindByIdAsync(id);

        public async Task<Role> FindByName(string roleName) => 
            await _roleManager.FindByNameAsync(roleName);
    }
}