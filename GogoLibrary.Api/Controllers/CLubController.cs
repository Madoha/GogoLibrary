using GogoLibrary.Domain.Dto.Club;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CLubController : ControllerBase
{
    private readonly IClubService _clubService;

    public CLubController(IClubService clubService)
    {
        _clubService = clubService;
    }

    [HttpGet("join-to-club")]
    public async Task<ActionResult> JoinToClub([FromQuery] long clubId)
    {
        var userName = await GetUser();
        var response = await _clubService.JoinToClubAsync(clubId, userName);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("leave-from-club")]
    public async Task<ActionResult> LeaveFromClub([FromQuery] long clubId)
    {
        var userName = await GetUser();
        var response = await _clubService.LeaveFromClubAsync(clubId, userName);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }
    
    [HttpPost("create-club")]
    public async Task<ActionResult> CreateClub([FromBody] CreateClubDto dto)
    {
        var response = await _clubService.CreateClubAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("get-all-clubs")]
    public async Task<ActionResult<CollectionResult<ClubDto>>> GetAllClubs()
    {
        var response = await _clubService.GetAllClubsAsync();
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    private async Task<string> GetUser()
    {
        var userName = User.Identity.Name;
        return userName;
    }
}