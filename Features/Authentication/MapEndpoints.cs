using Carter;
using dotnet_weather_app.Features.Authentication.Interfaces;
using dotnet_weather_app.Features.Authentication.Models;
using MediatR;
using System.Net;

namespace dotnet_weather_app.Features.Authentication;

public class MapEndpoints(IAuthService authService) : ICarterModule
{
  private readonly IAuthService _authService = authService;

  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("api/login", async (AuthRequest request) =>
    {
      var result = await _authService.Login(request);
      if (result.IsFailure)
      {
        if (result.Error.Code == HttpStatusCode.BadRequest)
        {
          return Results.BadRequest(result.Error.Message);
        }

        return Results.UnprocessableEntity(result.GenericErrorMessage());
      }
      
      return Results.Ok(result.Value);
    });
  }
}
