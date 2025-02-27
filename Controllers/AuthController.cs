using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using backendPFPU.Helpers;


[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly string _connectionString = "Data Source=DataBase/ProyectoFinalDB.db;Cache=Shared";
    private readonly PasswordService _passwordService;

    public AuthController(JwtService jwtService, PasswordService passwordService)
    {
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Consulta manual a la base de datos
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var query = "SELECT contrasenia, tipo, id_usuario, nombre, apellido FROM usuario WHERE dni = @Username";

            using (var command = new SqliteCommand(query, connection))
            {
                // Utiliza parámetros para evitar inyección SQL
                command.Parameters.AddWithValue("@Username", request.Dni);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) // Si encuentra el usuario
                    {
                        var hashedPassword = reader.GetString(0);
                        var tipo = reader.GetString(1);
                        var id_usuario = reader.GetInt64(2);
                        var nombre = reader.GetString(3);
                        var apellido = reader.GetString(4);

                        // Verifica la contraseña usando PasswordService
                        if (_passwordService.Verificar(hashedPassword, request.Password))
                        {
                            var token = _jwtService.GenerateToken(request.Dni);
                            return Ok(new { token, tipo, id_usuario, nombre, apellido });
                        }
                    }
                }
            }
        }

        return Unauthorized("Usuario o contraseña incorrectos");
    }

    // Modelo de solicitud de login
    public class LoginRequest
    {
        public string Dni { get; set; }
        public string Password { get; set; }
    }
}