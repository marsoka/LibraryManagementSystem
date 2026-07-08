using System.Security.Claims;
using Library.BLL.DTOs;
using Library.BLL.DTOs.AuthDTO;
using Library.BLL.Interfaces;
using Library.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return Ok(token);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _authService.RegisterUser(dto);
        return Created();
    }
}