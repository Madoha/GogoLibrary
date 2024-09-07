using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
    {
        var response = await _authService.RegisterUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<UserDto>>> Login([FromBody] LoginUserDto dto)
    {
        var response = await _authService.LoginUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}