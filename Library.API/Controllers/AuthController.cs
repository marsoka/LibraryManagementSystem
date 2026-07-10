using System.Security.Claims;
using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.BLL.Interfaces;
using Library.BLL.Services;
using Library.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _authService.RegisterUser(dto);
        return Created();
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        var refresh = await _authService.RefreshTokenAsync(refreshTokenRequest);
        return Ok(refresh);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest logoutRequest)
    {
        await _authService.Logout(User, logoutRequest);
        return Ok();
    }

}