using System.Text.RegularExpressions;

namespace Nexora.Api.Services;

public static class PasswordValidator
{
   
    private static readonly Regex Regla = new Regex(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$",
        RegexOptions.Compiled
    );

    public static bool EsValida(string password)
    {
        return !string.IsNullOrEmpty(password) && Regla.IsMatch(password);
    }

    public const string MensajeError =
        "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.";
}