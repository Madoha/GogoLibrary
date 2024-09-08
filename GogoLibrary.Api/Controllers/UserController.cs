using System.Security.Claims;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("get-profile")]
    public async Task<ActionResult<BaseResult<UserProfileDto>>> GetProfile()
    {
        var userName = User.Identity.Name;
        var response = await _userService.GetUserProfileAsync(userName);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("recommend-book")]
    public async Task<ActionResult<BaseResult>> RecommendBook([FromQuery] long bookId)
    {
        var userName = User.Identity.Name;
        var response = await _userService.RecommendBookAsync(userName, bookId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpGet("add-to-favorites")]
    public async Task<ActionResult<BaseResult>> AddToFavorites([FromQuery] long bookId)
    {
        var userName = User.Identity.Name;
        var response = await _userService.AddToFavoritesAsync(userName, bookId);
        if (response.IsSuccess)
            return Ok(response);
        return BadRequest(response);
    }
    
    [HttpGet("get-favorites")]
    public async Task<ActionResult<BaseResult>> GetFavorites()
    {
        var userName = User.Identity.Name;
        var response = await _userService.GetFavoritesAsync(userName);
        if (response.IsSuccess)
            return Ok(response);
        return BadRequest(response);
    }
}