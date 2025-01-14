using dotnet_weather_app.Features.Authentication.Models;
using dotnet_weather_app.Shared;

namespace dotnet_weather_app.Features.Authentication.Interfaces;

public interface IAuthService
{
  Task<Result<AuthResponse>> Login(AuthRequest request);
  Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
}
