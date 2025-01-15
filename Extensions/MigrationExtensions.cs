using dotnet_weather_app.Database;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace dotnet_weather_app.Extensions;

public static class MigrationExtensions
{
  public static void ApplyMigrations(this WebApplication app)
  {
    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

    using var scope = app.Services.CreateScope();

    // add here the remains dbContext
    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

    dbContext.Database.Migrate();
  }
}
