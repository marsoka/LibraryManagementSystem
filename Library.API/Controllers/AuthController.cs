using Library.BLL.DTOs;
using Library.BLL.Interfaces;
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

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        if (dto.Username != "admin" ||
            dto.Password != "123456")
        {
            return Unauthorized();
        }

        var token = _authService.GenerateToken(dto.Username);

        return Ok(token);
    }
}