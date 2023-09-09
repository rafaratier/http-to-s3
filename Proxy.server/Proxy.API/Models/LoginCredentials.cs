using System.ComponentModel.DataAnnotations;

namespace Proxy.API.Models;

public record LoginCredentials
{
    [Required(ErrorMessage = "Forneça o endereço de email para efetuar o login")]
    [EmailAddress(ErrorMessage = "Email em formato inválido.")]
    public string? Email { get; init; }
    
    [Required(ErrorMessage = "Forneça a senha para efetuar o login.")]
    [MinLength(6, ErrorMessage = "A senha precisa ter no mínimo 6 caracteres")]
    public string? Password { get; init; }
}