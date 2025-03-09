using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarCorreoBienvenida(string destinatario, string nombre, int dni, string contrasenia)
    {
        var emailSettings = _configuration.GetSection("SmtpSettings");

        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        mensaje.To.Add(new MailboxAddress("", destinatario));
        mensaje.Subject = "Bienvenido a la plataforma";

        mensaje.Body = new TextPart("html")
        {
            Text = $"<h2>Hola {nombre},</h2><p>¡Bienvenido a la plataforma! Tu cuenta ha sido creada con éxito.</p>" +
            $"<p> Tus credenciales de acceso son:\nDNI: {dni}\nContraseña: {contrasenia}\n\nRecuerda cambiar tu contraseña después de iniciar sesión en Perfil --> Cambiar Contraseña.\n\nSaludos. </p>"
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["Server"], int.Parse(emailSettings["Port"]), false);
        await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["Password"]);
        await smtp.SendAsync(mensaje);
        await smtp.DisconnectAsync(true);
    }
}
