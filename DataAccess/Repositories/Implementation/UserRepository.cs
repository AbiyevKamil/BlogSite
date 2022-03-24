using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Core.Helpers;
using DataAccess.Persistance.Contexts;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public UserRepository(BlogContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByClaimsAsync(ClaimsPrincipal claims)
        {
            return await _userManager.GetUserAsync(claims);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<string> GetEmailConfirmationToken(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        // Implement Later

        public Task<string> SendChangePasswordUrlAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(User user, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public Task AddProfilePictureAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}