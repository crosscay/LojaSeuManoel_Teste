using LojaSeuManoel.Application.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        // Geramos o token JWT
        var token = _authService.GenerateJwtToken();

        return Ok(new { token });
    }
}