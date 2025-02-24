using AuthwithIdentity.Models.DTOs.RequestesDto;
using AuthwithIdentity.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }

    [HttpGet("confirmemail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] int code)
    {
        var result = await _authService.ConfirmEmailAsync(email, code);
        return result;
    }

   
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ForgotPasswordAsync(model.Email);
        return Ok(new { Message = result });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ResetPasswordAsync(model);
        return Ok(new { Message = result });
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole([FromBody]  string roleName)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authService.CreateRoleAsync(roleName);
        return Ok(new { Message = result });
    }

    [HttpPost("add-user-to-role")]
    public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleDto addUserToRoleModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authService.AddUserToRoleAsync(addUserToRoleModel);
        return Ok(new { Message = result });
    }

    [HttpPost("remove-user-from-role")]
    public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleDto removeUserFromRoleModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _authService.RemoveUserFromRoleAsync(removeUserFromRoleModel);
        return Ok(new { Message = result });
    }
}
