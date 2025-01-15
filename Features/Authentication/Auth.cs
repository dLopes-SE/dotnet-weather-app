using Carter;
using dotnet_weather_app.Features.Authentication.Interfaces;
using dotnet_weather_app.Features.Authentication.Models;
using dotnet_weather_app.Shared;
using MediatR;
using Mapster;
using System.Net;

namespace dotnet_weather_app.Features.Authentication;

public static class Auth
{
  public class Command : LoginRequest, IRequest<Result<LoginResponse>> 
  {
    
  }

  internal sealed class Handler(IAuthService authService) : IRequestHandler<Command, Result<LoginResponse>>
  {
    private readonly IAuthService _authService = authService;
    public async Task<Result<LoginResponse>> Handle(Command request, CancellationToken cancellationToken)
    {
      return await _authService.Login(request);
    }
  }
}

public class LoginEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("api/login", async (LoginRequest request, ISender sender) =>
    {
      var command = request.Adapt<Auth.Command>();

      var result = await sender.Send(command);
      if (result.IsFailure)
      {
        if (result.Error.Code == HttpStatusCode.BadRequest)
        {
          return Results.BadRequest(result.Error.Message);
        }

        return Results.UnprocessableEntity(result.GetGenericErrorMessage());
      }

      return Results.Ok(result.Value);
    });
  }
}
