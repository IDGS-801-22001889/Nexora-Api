namespace Nexora.Api.DTOs;

public class RegisterDto
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Rol { get; set; } // opcional, default "Cliente"
}

public class LoginDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
}

public class ActualizarPerfilDto
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class CambiarPasswordDto
{
    public string PasswordActual { get; set; } = null!;
    public string PasswordNueva { get; set; } = null!;
}

public class SolicitudClienteDto
{
    public string NombreEmpresa { get; set; } = null!;
    public string NombreContacto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? Mensaje { get; set; }
}

public class ActualizarUsuarioDto
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public bool Activo { get; set; }
}

public class CrearUsuarioDto
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Rol { get; set; } = null!;
}