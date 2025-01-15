using dotnet_weather_app.Features.Authentication.Interfaces;
using dotnet_weather_app.Features.Authentication.Models;
using dotnet_weather_app.Shared;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace dotnet_weather_app.Features.Authentication.Services;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
{
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly JwtSettings _jwtSettings = jwtSettings.Value;
  private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

  public async Task<Result<LoginResponse>> Login(LoginRequest request)
  {
    var hasher = new PasswordHasher<ApplicationUser>();
    // Verify if the user exists already
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user is null)
    {
      return Result.Failure<LoginResponse>(new Error(HttpStatusCode.BadRequest, "User does not exist"));
    }

    // Verify if the password is correct
    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    if (!result.Succeeded)
    {
      return Result.Failure<LoginResponse>(new Error(HttpStatusCode.BadRequest, "Invalid password"));
    }

    // Generate the token
    var token = await GenerateToken(user);

    return new LoginResponse()
    {
      Id = user.Id,
      UserName = user.UserName,
      Email = user.Email,
      Token = new JwtSecurityTokenHandler().WriteToken(token)
    };
  }

  public async Task<Result<RegistrationResponse>> Register(RegistrationRequest request)
  {
    if (await _userManager.FindByEmailAsync(request.Email) is not null)
    {
      return Result.Failure<RegistrationResponse>(new Error(HttpStatusCode.BadRequest, "User already exists"));
    };

    var user = request.Adapt<ApplicationUser>();
    var result = await _userManager.CreateAsync(user, request.Password);
    if (!result.Succeeded)
    {
      return Result.Failure<RegistrationResponse>(new Error(HttpStatusCode.UnprocessableEntity, "Failed to create user"));
    }

    return new RegistrationResponse()
    {
      UserId = user.Id
    };
  }

  private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
  {
    // get claims and roles
    var userClaims = await _userManager.GetClaimsAsync(user);
    var roles = await _userManager.GetRolesAsync(user);

    // Assign the roles as a claim
    var userRoles = roles.Select(role => new Claim(ClaimTypes.Role, role));

    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      new Claim(JwtRegisteredClaimNames.Email, user.Email),
      new Claim("uid", user.Id)
    }
    .Union(userClaims)
    .Union(userRoles);

    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


    return new JwtSecurityToken(
      issuer: _jwtSettings.Issuer,
      audience: _jwtSettings.Audience,
      claims: claims,
      expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
      signingCredentials: signingCredentials);
  }
}
