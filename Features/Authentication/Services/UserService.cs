using dotnet_weather_app.Features.Authentication.Interfaces;
using dotnet_weather_app.Models;
using System.Security.Claims;

namespace dotnet_weather_app.Features.Authentication.Services;

public class UserService(IHttpContextAccessor contextAccessor) : IUserService
{
  private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

  public string UserId => _contextAccessor.HttpContext?.User?.FindFirstValue("uid") ?? string.Empty;

  public Task<IEnumerable<User>> GetUsers()
  {
    throw new NotImplementedException();
  }
}
