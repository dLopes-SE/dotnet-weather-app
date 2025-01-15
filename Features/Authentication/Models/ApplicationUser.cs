using Microsoft.AspNetCore.Identity;

namespace dotnet_weather_app.Features.Authentication.Models;

public class ApplicationUser : IdentityUser
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
