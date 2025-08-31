using AutomiqoSoftware.DTOs.AuthorizationDTO.Users;
using AutomiqoSoftware.Interfaces.AuthorizationInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomiqoSoftware.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto dto) {
        var result = await _authService.RegisterUser(dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto dto) {
        var result = await _authService.UserLogin(dto);

        if (result) {
            return Ok(result);
        }
        else {
            return Unauthorized("The request lacks valid authentication " +
                "credentials for the requested resource.");
        }
    }
}