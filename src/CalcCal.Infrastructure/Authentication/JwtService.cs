using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CalcCal.Application.Abstractions.Authentication;
using CalcCal.Domain.Users;
using CalcCal.Infrastructure.Authentication.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Responses.DB;

namespace CalcCal.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    private readonly AuthenticationOptions _options;
    private readonly IPasswordService _passwordService;

    public JwtService(IOptions<AuthenticationOptions> options, IPasswordService passwordService)
    {
        _passwordService = passwordService;
        _options = options.Value;
    }

    public async Task<Result<string>> GetAccessTokenAsync(
        User userToAuthenticate,
        string password,
        CancellationToken cancellationToken = default)
    {
        var isPasswordCorrect = _passwordService.VerifyPassword(password, userToAuthenticate.PasswordHash);

        return isPasswordCorrect ? 
            Generate(userToAuthenticate) : 
            Result.Failure<string>(AuthenticationErrors.AuthenticationFailed);
    }

    private string Generate(User user)
    {
        var claims = UserRepresentationModel
            .FromUser(user)
            .ToClaims();

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddMinutes(_options.ExpireInMinutes),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}