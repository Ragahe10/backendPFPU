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

    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            // Obtener la contraseña actual del usuario
            var getPasswordQuery = "SELECT contrasenia FROM usuario WHERE id_usuario = @UserId";
            using (var getPasswordCommand = new SqliteCommand(getPasswordQuery, connection))
            {
                getPasswordCommand.Parameters.AddWithValue("@UserId", request.IdUsuario);

                var hashedPassword = getPasswordCommand.ExecuteScalar() as string;
                if (hashedPassword == null || !_passwordService.Verificar(hashedPassword, request.OldPassword))
                {
                    return Unauthorized("Contraseña actual incorrecta");
                }
            }

            // Hashear la nueva contraseña
            var newHashedPassword = _passwordService.Encriptar(request.NewPassword);

            // Actualizar la contraseña en la base de datos
            var updatePasswordQuery = "UPDATE usuario SET contrasenia = @NewPassword WHERE id_usuario = @UserId";
            using (var updatePasswordCommand = new SqliteCommand(updatePasswordQuery, connection))
            {
                updatePasswordCommand.Parameters.AddWithValue("@NewPassword", newHashedPassword);
                updatePasswordCommand.Parameters.AddWithValue("@UserId", request.IdUsuario);

                int rowsAffected = updatePasswordCommand.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Ok("Contraseña actualizada correctamente");
                }
                else
                {
                    return BadRequest("Error al actualizar la contraseña");
                }
            }
        }
    }

    // Modelo de solicitud de login
    public class LoginRequest
    {
        public string Dni { get; set; }
        public string Password { get; set; }
    }

    // Modelo de solicitud para cambio de contraseña
    public class ChangePasswordRequest
    {
        public long IdUsuario { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

