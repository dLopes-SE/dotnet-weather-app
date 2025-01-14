using Microsoft.AspNetCore.Identity;

namespace dotnet_weather_app.Features.Authentication.Models;

public class ApplicationUser : IdentityUser
  {
      public string Id { get; set; }
      public string Email { get; set; }
      public string PasswordHash { get; set; }
  }
