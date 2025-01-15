using dotnet_weather_app.Features.Authentication.Models;
using dotnet_weather_app.Shared;

namespace dotnet_weather_app.Features.Authentication.Interfaces;

public interface IAuthService
{
  Task<Result<LoginResponse>> Login(LoginRequest request);
  Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
}
