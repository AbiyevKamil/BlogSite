using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Service.Business.Abstract
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<User> FindByClaimsAsync(ClaimsPrincipal claims);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<bool> IsInRoleAsync(User user, string role);
        Task<IdentityResult> AddToRoleAsync(User user, string role);
        Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles);
        Task<IList<string>> GetRolesAsync(User user);
        Task<string> GetEmailConfirmationToken(User user);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<IdentityResult> UpdateUserAsync(User user);

        // Implement Later
        Task DeleteUserAsync(User user);
        Task<string> SendChangePasswordUrlAsync(User user);
        Task<bool> ChangePasswordAsync(User user, string newPassword);
        Task AddProfilePictureAsync(User user);
    }
}