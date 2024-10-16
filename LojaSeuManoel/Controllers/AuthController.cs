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
        // Generamos el token JWT sin userId
        var token = _authService.GenerateJwtToken();

        return Ok(new { token });
    }
}