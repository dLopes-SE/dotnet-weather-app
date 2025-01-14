using System.Net;

namespace dotnet_weather_app.Shared;

public record Error(HttpStatusCode Code, string Message)
{
  public static readonly Error None = new(HttpStatusCode.OK, string.Empty);

}