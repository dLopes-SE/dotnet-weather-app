using dotnet_weather_app.Models;

namespace dotnet_weather_app.Features.Authentication.Interfaces;

public interface IUserService
{
  Task<IEnumerable<User>> GetUsers();
  public string UserId { get; }
}
