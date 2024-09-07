using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GogoLibrary.Domain.Dto.Token;
using GogoLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using GogoLibrary.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GogoLibrary.Application.Services;

public class TokenService : ITokenService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly string _jwtKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IBaseRepository<User> userRepository,
        IOptions<JwtSettings> options)
    {
        _userRepository = userRepository;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _jwtKey = options.Value.JwtKey;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityToken =
            new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(15), credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumbers = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumbers);
        return Convert.ToBase64String(randomNumbers);
    }

    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidateLifetime = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return claimsPrincipal;
    }

    public async Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto)
    {
        var accessToken = dto.AccessToken;
        var refreshToken = dto.RefreshToken;
        
        var claimsPrincipal = GetClaimsPrincipalFromExpiredToken(accessToken);
        var userName = claimsPrincipal.Identity?.Name;
        
        var user = await _userRepository.GetAll().Include(u => u.UserToken).FirstOrDefaultAsync(u => u.UserName == userName);
        if (user == null || user.UserToken.RefreshToken != refreshToken || user.UserToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = "Invalid client request"
            };
        }
        
        var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        user.UserToken.RefreshToken = newRefreshToken;
        _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            }
        };
    }
}