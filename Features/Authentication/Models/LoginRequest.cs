using System.ComponentModel.DataAnnotations;

namespace dotnet_weather_app.Features.Authentication.Models;

public class LoginRequest
{
  [Required]
  public string Email { get; set; }
  [Required]
  public string Password { get; set; }
}
