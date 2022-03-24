using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using DataAccess.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Identity;
using Service.Business.Abstract;

namespace Service.Business.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _unitOfWork.Users.CreateAsync(user,  password);
        }

        public async Task<User> FindByClaimsAsync(ClaimsPrincipal claims)
        {
            return await _unitOfWork.Users.FindByClaimsAsync(claims);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _unitOfWork.Users.FindByEmailAsync(email);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _unitOfWork.Users.FindByUserNameAsync(userName);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _unitOfWork.Users.CheckPasswordAsync(user, password);
        }

        public async Task<bool> IsInRoleAsync(User user, string role)
        {
            return await _unitOfWork.Users.IsInRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return await _unitOfWork.Users.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
        {
            return await _unitOfWork.Users.AddToRolesAsync(user, roles);
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _unitOfWork.Users.GetRolesAsync(user);
        }

        public async Task<string> GetEmailConfirmationToken(User user)
        {
            return await _unitOfWork.Users.GetEmailConfirmationToken(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await _unitOfWork.Users.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _unitOfWork.Users.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _unitOfWork.Users.DeleteUserAsync(user);
        }

        public async Task<string> SendChangePasswordUrlAsync(User user)
        {
            return await _unitOfWork.Users.SendChangePasswordUrlAsync(user);
        }

        public async Task<bool> ChangePasswordAsync(User user, string newPassword)
        {
            return await _unitOfWork.Users.ChangePasswordAsync(user, newPassword);
        }

        public async Task AddProfilePictureAsync(User user)
        {
            await _unitOfWork.Users.AddProfilePictureAsync(user);
        }
    }
}