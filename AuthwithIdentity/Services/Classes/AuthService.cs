using AuthwithIdentity.DbContext;
using AuthwithIdentity.Exceptions;
using AuthwithIdentity.JwtOptions;
using AuthwithIdentity.Models;
using AuthwithIdentity.Models.DTOs.RequestesDto;
using AuthwithIdentity.Models.DTOs.ResponsesDto;
using AuthwithIdentity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace AuthwithIdentity.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _context = dbContext;
        }

        public async Task<AuthReponseDto> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                throw new BadRequestException("This Email Is already registered!");

            if (await _userManager.FindByNameAsync(model.Name) is not null)
                throw new BadRequestException("This Username Is already registered!");

            var user = new ApplicationUser
            {
                UserName = model.Name,
                Email = model.Email,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException($"User Creation Failed: {string.Join(", ", errors)}");
            }

            await _userManager.AddToRoleAsync(user, "User");

            var emailCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // Here you can send the token via email to confirm the account
            string asendEmail = sendEmail(user.Email, emailCode);
            return new AuthReponseDto
            {
                Email = user.Email,
                IsAuthentcated = true,
                Roles = new List<string> { "User" },
                Username = user.UserName,
                Message = "Registration successful. Please check your email to confirm your account."
            };
        }
        public async Task<IActionResult> ConfirmEmailAsync(string email, int code)
        {
            if (email == null || code == 0)
            {
                return new BadRequestObjectResult("Invalid email or code");
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new BadRequestObjectResult("Invalid email or code");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code.ToString());
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult("Invalid email or code");
            }
            else
            {
                return new OkObjectResult("Email confirmed successfully");
            }
        }
        public async Task<AuthReponseDto> LoginAsync(LoginDto Dto)
        {
            if (Dto == null)
                throw new BadRequestException("Invalid login request.");

            var user = await _userManager.FindByNameAsync(Dto.UserName);
            if (user is null || !await _userManager.CheckPasswordAsync(user, Dto.Password))
                throw new BadRequestException("Email or Password is incorrect!");

            if (!user.EmailConfirmed)
                throw new BadRequestException("Email not confirmed. Please check your email.");

            var jwtSecurityToken = await _tokenService.CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            return new AuthReponseDto
            {
                IsAuthentcated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                Username = user.UserName,
                Roles = roleList.ToList(),
                RefreshToken = await _tokenService.GenerateAndStoreRefreshToken(user, _context)
            };
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new BadRequestException("User with this email does not exist.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);



            sendEmail(email, token);

            return "Reset password link has been sent to your email.";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new BadRequestException("Invalid email.");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                throw new BadRequestException($"Password reset failed: {string.Join(", ", errors)}");
            }

            return "Password has been reset successfully.";
        }


        public async Task<string> CreateRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists) return "Role already exists.";

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
                return "Role created successfully.";
            else
            {
                return "Role creation failed.";
            }
        }

        public async Task<string> AddUserToRoleAsync(AddUserToRoleDto addUserToRoleModel)
        {
            var user = await _userManager.FindByEmailAsync(addUserToRoleModel.email);
            if (user == null)
                return "User not found.";
            var role = await _roleManager.FindByNameAsync(addUserToRoleModel.roleName);
            if (role == null)
                return "Role not found.";
            var result = await _userManager.AddToRoleAsync(user, addUserToRoleModel.roleName);
            if (result.Succeeded)
                return "User added to role successfully.";
            else
                return "User addition to role failed.";
        }

        public async Task<string> RemoveUserFromRoleAsync(RemoveUserFromRoleDto removeUserFromRoleModel)
        {
            var user = await _userManager.FindByEmailAsync(removeUserFromRoleModel.email);
            if (user == null)
                return "User not found.";
            var role = await _roleManager.FindByNameAsync(removeUserFromRoleModel.roleName);
            if (role == null)
                return "Role not found.";
            var result = await _userManager.RemoveFromRoleAsync(user, removeUserFromRoleModel.roleName);
            if (result.Succeeded)
                return "User removed from role successfully.";
            else
                return "User removal from role failed.";
        }
        private string sendEmail(string email, string emailCode)
        {
            StringBuilder emailmessage = new StringBuilder();
            emailmessage.Append("<h1>Confirm your email </h1>");
            emailmessage.Append("<p> Copy This Email Code  to confirm your email:</p>");
            emailmessage.Append($"<p> {email}=token={emailCode}'>Confirm Email</p>");

            string message = emailmessage.ToString();
            var _email = new MimeMessage();

            _email.To.Add(MailboxAddress.Parse(email));  // تم تصحيحه
            _email.From.Add(MailboxAddress.Parse("christop.rogahn@ethereal.email"));
            _email.Subject = "Confirm your email";
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("christop.rogahn@ethereal.email", "ZYvJwqqcbBfxgynFne");
                smtp.Send(_email);
                smtp.Disconnect(true);
            }

            return "Thank you for your registration, kindly check your email for confirmation link.";
        }


    }
}





