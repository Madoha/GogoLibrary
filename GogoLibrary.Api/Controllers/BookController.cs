using GogoLibrary.AI.Services;
using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GogoLibrary.Api.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookAIService _bookAiService;
    private readonly IBookService _bookService;

    public BookController(IBookAIService bookAiService, 
        IBookService bookService)
    {
        _bookAiService = bookAiService;
        _bookService = bookService;
    }

    [HttpPost("find-by-description")]
    public async Task<ActionResult<BaseResult<string>>> FindByDescription([FromBody] FindByDescriptionBookDto dto)
    {
        var response = await _bookAiService.FindByDescription(dto.Description);
        if (response.IsSuccess)
            return Ok(response);
        return BadRequest(response);
    }

    // [HttpPost("search-by-ai")]
    // public async Task<ActionResult<CollectionResult<SearchBookResultDto>>> SearchByAi([FromBody] SearchBookDto dto)
    // {
    //     var response = await _bookService.SearchBooksAsync(dto);
    //     if (response.IsSuccess)
    //         return Ok(response);
    //     return BadRequest(response);
    // }
    
    [HttpPost("search")]
    public async Task<ActionResult<CollectionResult<SearchBookResultDto>>> Search([FromBody] SearchBookDto dto)
    {
        var response = await _bookService.SearchBooksInDbAsync(dto);
        if (response.IsSuccess)
            return Ok(response);
        return BadRequest(response);
    }

    [HttpPost("add-book")]
    public async Task<ActionResult<BaseResult>> AddBook([FromBody] CreateBookDto dto)
    {
        var userName = User.Identity.Name;
        var response = await _bookService.AddBookAsync(dto, userName);
        if (response.IsSuccess)
            return Ok(response);
        return BadRequest(response);
    }
}