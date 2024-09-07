using GogoLibrary.Domain.Dto.Comment;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentServices _commentServices;

    public CommentController(ICommentServices commentServices)
    {
        _commentServices = commentServices;
    }

    [HttpPost("create")]
    public async Task<ActionResult<BaseResult>> AddCommentToBook([FromBody] AddCommentDto dto)
    {
        var response = await _commentServices.AddCommentToBookAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpGet("get-comments")]
    public async Task<ActionResult<BaseResult<CommentDto>>> GetAllCommentsByBookId([FromQuery] long bookId)
    {
        var response = await _commentServices.GetCommentsByBookIdAsync(bookId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}