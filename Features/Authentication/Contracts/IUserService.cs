using dotnet_weather_app.Features.Authentication.Models;

namespace dotnet_weather_app.Features.Authentication.Interfaces;

public interface IUserService
{
  Task<IEnumerable<ApplicationUser>> GetUsers();
  public string UserId { get; }
}
