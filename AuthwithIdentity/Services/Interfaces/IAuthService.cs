using AuthwithIdentity.Models.DTOs.RequestesDto;
using AuthwithIdentity.Models.DTOs.ResponsesDto;
using Microsoft.AspNetCore.Mvc;

namespace AuthwithIdentity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthReponseDto> RegisterAsync(RegisterDto Dto);
        Task<IActionResult> ConfirmEmailAsync(string email, int code );
        Task<AuthReponseDto> LoginAsync(LoginDto Dto);

        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordDto model);

        Task<string> CreateRoleAsync(string roleName);
        Task<string> AddUserToRoleAsync(AddUserToRoleDto addUserToRoleModel);

        Task<string> RemoveUserFromRoleAsync(RemoveUserFromRoleDto removeUserFromRoleModel);
        //Task<string> AddRloeAsync(AddRoleModel addRoleModel);
        //Task<AuthModel> RefreshTokenAsync(string refreshToken);
        //Task<bool> RevokeTokenAsync(string refreshToken);
    }
}

