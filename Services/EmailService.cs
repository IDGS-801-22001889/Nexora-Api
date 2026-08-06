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

    public void EnviarMensajeContacto(string nombre, string email, string telefono, string asunto, string mensaje)
    {
        var mensajeCorreo = new MailMessage
        {
            From = new MailAddress(_config["Email:From"]!, "Formulario de Contacto - NEXORA"),
            Subject = $"[Contacto] {asunto}",
            IsBodyHtml = true,
            Body = $@"
            <p><b>Nuevo mensaje desde el formulario de contacto:</b></p>
            <p><b>Nombre:</b> {nombre}<br/>
            <b>Correo:</b> {email}<br/>
            <b>Teléfono:</b> {telefono}<br/>
            <b>Asunto:</b> {asunto}</p>
            <p><b>Mensaje:</b><br/>{mensaje}</p>
        "
        };

        // Se envía A la misma cuenta configurada como remitente
        mensajeCorreo.To.Add(_config["Email:From"]!);
        mensajeCorreo.ReplyToList.Add(new MailAddress(email)); // para que puedas responder directo al cliente

        using var cliente = new SmtpClient(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]!))
        {
            Credentials = new NetworkCredential(_config["Email:From"], _config["Email:Password"]),
            EnableSsl = true
        };

        cliente.Send(mensajeCorreo);
    }
}