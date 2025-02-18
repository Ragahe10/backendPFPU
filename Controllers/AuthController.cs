using Microsoft.AspNetCore.Mvc;
using backendPFPU.Helpers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Simulación de validación de usuario (reemplázalo con una consulta real a la base de datos)
        if (request.Username == "admin" && request.Password == "1234")
        {
            var token = _jwtService.GenerateToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized("Usuario o contraseña incorrectos");
    }
}

// Modelo de solicitud de login
public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
