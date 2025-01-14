using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dotnet_weather_app.Features.Authentication.Models;

public class AuthResponse
{
  public string Id { get; set; }
  public string UserName { get; set; }
  public string Email { get; set; }
  public string Token { get; set; }
}
