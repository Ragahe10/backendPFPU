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

    // Nuevo método para enviar correos con deudas vencidas
    public async Task EnviarCorreoDeudaVencida(string destinatario, string nombre, float monto, string fechaVencimiento)
    {
        var emailSettings = _configuration.GetSection("SmtpSettings");

        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        mensaje.To.Add(new MailboxAddress("", destinatario));
        mensaje.Subject = "Aviso de Deuda Vencida";

        mensaje.Body = new TextPart("html")
        {
            Text = $"<h2>Hola {nombre},</h2>" +
                   $"<p>Te informamos que tienes una deuda vencida desde el <strong>{fechaVencimiento}</strong> con un monto de <strong>${monto}</strong>.</p>" +
                   "<p>Te pedimos que regularices tu situación lo antes posible para evitar recargos adicionales.</p>" +
                   "<p>Para más información, comunícate con administración.</p>" +
                   "<p>Saludos,</p><p>Administración de la Escuela</p>"
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["Server"], int.Parse(emailSettings["Port"]), false);
        await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["Password"]);
        await smtp.SendAsync(mensaje);
        await smtp.DisconnectAsync(true);
    }

    // Método para notificar al alumno sobre una nueva nota o modificación
    public async Task EnviarCorreoNotaActualizada(string destinatario, string nombre, string materia, float nota, string fecha, int trimestre)
    {
        string nombreTrimestre;
        switch (trimestre)
        {
            case 1:
                nombreTrimestre = "Primero";
                break;
            case 2:
                nombreTrimestre = "Segundo";
                break;
            case 3:
                nombreTrimestre = "Tercero";
                break;
            case 0:
                nombreTrimestre = "Recuperatorio";
                break;
            default:
                nombreTrimestre = "Desconocido";
                break;
        }

        var emailSettings = _configuration.GetSection("SmtpSettings");

        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        mensaje.To.Add(new MailboxAddress("", destinatario));
        mensaje.Subject = "Actualización de Nota";

        mensaje.Body = new TextPart("html")
        {
            Text = $"<h2>Hola {nombre},</h2>" +
                   $"<p>Se ha registrado una nueva nota o se ha actualizado una existente en la materia <strong>{materia}</strong> correspondiente al <strong>trimestre {nombreTrimestre}</strong>.</p>" +
                   $"<p><strong>Nota:</strong> {nota}</p>" +
                   $"<p><strong>Fecha de carga:</strong> {fecha}</p>" +
                   "<p>Para más detalles, revisa tu perfil en la plataforma.</p>" +
                   "<p>Saludos,</p><p>Administración Académica</p>"
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings["Server"], int.Parse(emailSettings["Port"]), false);
        await smtp.AuthenticateAsync(emailSettings["SenderEmail"], emailSettings["Password"]);
        await smtp.SendAsync(mensaje);
        await smtp.DisconnectAsync(true);
    }


}
