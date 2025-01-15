using System.ComponentModel.DataAnnotations;

namespace dotnet_weather_app.Features.Authentication.Models;

public class RegistrationRequest
{
  [Required]
  public string UserName { get; set; }
  [Required]
  public string Email { get; set; }
  [Required]
  [MinLength(6)]
  public string Password { get; set; }
  [Required]
  public string FirstName { get; set; }
  [Required]
  public string LastName { get; set; }
}
