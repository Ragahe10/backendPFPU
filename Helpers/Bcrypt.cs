using BCrypt.Net;
namespace backendPFPU.Helpers;

public class PasswordService
{
    public string Encriptar(string password)
    {
        // Genera un hash con un salt automático
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verificar(string hashedPassword, string providedPassword)
    {
        // Compara la contraseña proporcionada con el hash almacenado
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}