using Carter;
using dotnet_weather_app.Features.Authentication.Interfaces;
using dotnet_weather_app.Features.Authentication.Models;
using dotnet_weather_app.Shared;
using MediatR;
using Mapster;
using System.Net;

namespace dotnet_weather_app.Features.Authentication;

public static class Registration
{
  public class Command : RegistrationRequest, IRequest<Result<RegistrationResponse>>
  {

  }

  internal sealed class Handler(IAuthService authService) : IRequestHandler<Command, Result<RegistrationResponse>>
  {
    private readonly IAuthService _authService = authService;
    public async Task<Result<RegistrationResponse>> Handle(Command request, CancellationToken cancellationToken)
    {
      return await _authService.Register(request);
    }
  }
}

public class RegistrationEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("api/register", async (RegistrationRequest request, ISender sender) =>
    {
      var command = request.Adapt<Registration.Command>();

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
