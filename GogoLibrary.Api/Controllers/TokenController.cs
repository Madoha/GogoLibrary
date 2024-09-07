using GogoLibrary.Domain.Dto.Token;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }
    
    [HttpPost("refresh-token")]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto dto)
    {
        var response = await _tokenService.RefreshToken(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}