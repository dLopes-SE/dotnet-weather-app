using System.ComponentModel.DataAnnotations;

namespace dotnet_weather_app.Features.Authentication.Models;

public class AuthRequest
{
  [Required]
  public string Email { get; set; }
  [Required]
  public string Password { get; set; }
}
