using Microsoft.AspNetCore.Mvc;
using Nexora.Api.DTOs;
using Nexora.Api.Services;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactoController : ControllerBase
{
    private readonly EmailService _emailService;

    public ContactoController(EmailService emailService)
    {
        _emailService = emailService;
    }

    // POST api/contacto -> pública, envía el mensaje al correo de la empresa
    [HttpPost]
    public IActionResult Enviar(ContactoDto dto)
    {
        try
        {
            _emailService.EnviarMensajeContacto(dto.Nombre, dto.Email, dto.Telefono, dto.Asunto, dto.Mensaje);
            return Ok(new { mensaje = "Tu mensaje fue enviado correctamente." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"No se pudo enviar el mensaje: {ex.Message}");
        }
    }
}