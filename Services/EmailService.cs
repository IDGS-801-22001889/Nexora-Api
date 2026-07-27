using System.Net;
using System.Net.Mail;

namespace Nexora.Api.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void EnviarCredenciales(string nombreDestino, string emailDestino, string passwordTemporal)
    {
        var mensaje = new MailMessage
        {
            From = new MailAddress(_config["Email:From"]!, "NEXORA Business"),
            Subject = "Tus accesos a NEXORA Business",
            IsBodyHtml = true,
            Body = $@"
                <p>Hola {nombreDestino},</p>
                <p>Tu cuenta en el portal de NEXORA Business ha sido creada.</p>
                <p><b>Correo:</b> {emailDestino}<br/>
                <b>Contraseña temporal:</b> {passwordTemporal}</p>
                <p>Te recomendamos cambiar tu contraseña al iniciar sesión por primera vez desde tu perfil.</p>
            "
        };
        mensaje.To.Add(emailDestino);

        using var cliente = new SmtpClient(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]!))
        {
            Credentials = new NetworkCredential(_config["Email:From"], _config["Email:Password"]),
            EnableSsl = true
        };

        cliente.Send(mensaje);
    }
}