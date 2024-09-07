using System.Security.Claims;
using GogoLibrary.Domain.Dto.Token;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    public string GenerateRefreshToken();
    public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string accessToken);
    public Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
}