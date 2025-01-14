using dotnet_weather_app.Features.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_weather_app.Features.Authentication.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
  public void Configure(EntityTypeBuilder<ApplicationUser> builder)
  {
    var hasher = new PasswordHasher<ApplicationUser>();
    builder.HasData(
      new ApplicationUser
      {
        Id = "a47c7f64-60aa-4733-9e9e-946198d9b522",
        Email = "admin@localhost.com",
        NormalizedEmail = "ADMIN@LOCALHOST.COM",
        UserName = "admin@localhost.com",
        NormalizedUserName = "ADMIN@LOCALHOST.COM",
        PasswordHash = hasher.HashPassword(null, "Password@1"),
        EmailConfirmed = true
      });
  }
}
