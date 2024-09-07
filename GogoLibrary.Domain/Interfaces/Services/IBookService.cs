using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IBookService
{
    // Task<CollectionResult<BookCsv>> GetBooksAsync();
    // Task<BaseResult<BookCsv>> SearchBooksAsync(SearchBookDto searchBookDto);
    Task<CollectionResult<SearchBookResultDto>> SearchBooksInDbAsync(SearchBookDto dto);
    // Task<BaseResult> AddToOwnDbAsync(IEnumerable<BookCsv> books);
}