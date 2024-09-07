using System.Globalization;
using System.Net;
using System.Text;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Application.Services;

public class BookService : IBookService
{
    // private readonly string _csvFilePath = "../database.csv";
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBookAIService _bookAIService;
    private readonly IMapper _mapper;
    
    public BookService(IBaseRepository<Book> bookRepository, 
        IBookAIService bookAiService, 
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _bookAIService = bookAiService;
        _mapper = mapper;
    }

//     public async Task<CollectionResult<BookCsv>> GetBooksAsync()
//     {
//         // if (!File.Exists(_csvFilePath))
//         // {
//         //     throw new FileNotFoundException($"CSV файл не найден по пути: {_csvFilePath}");
//         // }
//         
//         var config = new CsvConfiguration(CultureInfo.InvariantCulture)
//         {
//             HeaderValidated = null,
//             MissingFieldFound = null 
//         };
//         
//         using var reader = new StreamReader(_csvFilePath, Encoding.UTF8);
//         using var csv = new CsvReader(reader, config);
//
//         var records = new List<BookCsv>();
//         await foreach (var record in csv.GetRecordsAsync<BookCsv>())
//         {
//             records.Add(record);
//         }
//
//         return new CollectionResult<BookCsv>()
//         {
//             Data = records,
//             Count = records.Count
//         };
//     }
//
//     public async Task<BaseResult<BookCsv>> SearchBooksAsync(SearchBookDto searchBookDto)
// {
//     try
//     {
//         // Получаем все книги из CSV
//         var booksResult = await GetBooksAsync();
//         var books = booksResult.Data.AsQueryable();
//
//         // Применяем фильтры
//         if (!string.IsNullOrWhiteSpace(searchBookDto.BookTitle))
//         {
//             var translatedTitle = await _bookAIService.TranslatePrompt(searchBookDto.BookTitle, "en");
//             books = books.Where(b => b.Book_title.ToLower(CultureInfo.InvariantCulture)
//                 .Contains(translatedTitle.Data.Replace("\n", "").Replace("\r", "").ToLower(CultureInfo.InvariantCulture)));
//         }
//
//         if (!string.IsNullOrWhiteSpace(searchBookDto.YearOfPublication))
//         {
//             books = books.Where(b => b.Year_of_publication.Contains(searchBookDto.YearOfPublication));
//         }
//
//         if (!string.IsNullOrWhiteSpace(searchBookDto.Publisher))
//         {
//             var translatedPublisher = await _bookAIService.TranslatePrompt(searchBookDto.Publisher, "en");
//             books = books.Where(b => b.Publisher.ToLower(CultureInfo.InvariantCulture)
//                 .Contains(translatedPublisher.Data.Replace("\n", "").Replace("\r", "").ToLower(CultureInfo.InvariantCulture)));
//         }
//
//         var firstBook = books.FirstOrDefault();
//         if (firstBook != null)
//         {
//             await AddToOwnDbAsync(new[] { firstBook });
//             return new BaseResult<BookCsv>
//             {
//                 Data = firstBook
//             };
//         }
//
//         return new BaseResult<BookCsv>
//         {
//             ErrorMessage = "No books found",
//             ErrorCode = (int)HttpStatusCode.NotFound
//         };
//     }
//     catch (Exception ex)
//     {
//         return new BaseResult<BookCsv>
//         {
//             ErrorMessage = $"Internal server error: {ex.Message}",
//             ErrorCode = (int)HttpStatusCode.InternalServerError
//         };
//     }
// }

    public async Task<CollectionResult<SearchBookResultDto>> SearchBooksInDbAsync(SearchBookDto searchBookDto)
    {
        try
        {
            var query = _bookRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchBookDto.BookTitle))
            {
                var title = "%" + searchBookDto.BookTitle + "%";
                query = query.Where(b => EF.Functions.Like(b.Title, title));
            }

            if (!string.IsNullOrWhiteSpace(searchBookDto.BookAuthor))
            {
                var author = "%" + searchBookDto.BookAuthor + "%";
                query = query.Where(b => EF.Functions.Like(b.Author, author));
            }

            if (!string.IsNullOrWhiteSpace(searchBookDto.YearOfPublication))
            {
                query = query.Where(b => b.YearOfPublication.Contains(searchBookDto.YearOfPublication));
            }

            if (!string.IsNullOrWhiteSpace(searchBookDto.Publisher))
            {
                var publisher = "%" + searchBookDto.Publisher + "%";
                query = query.Where(b => EF.Functions.Like(b.Publisher, publisher));
            }

            var books = await query.ToListAsync();
            var result = _mapper.Map<IEnumerable<SearchBookResultDto>>(books);
            var count = books.Count();

            return new CollectionResult<SearchBookResultDto>()
            {
                Data = result,
                Count = count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResult<SearchBookResultDto>()
            {
                ErrorMessage = "Ошибка при поиске книг в базе данных",
                ErrorCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    // public async Task<BaseResult> AddToOwnDbAsync(IEnumerable<BookCsv> books)
    // {
    //     if (books.Count() == 0)
    //         throw new Exception("Can not save empty books");
    //     
    //     var firstBook = books.FirstOrDefault();
    //
    //     if (firstBook != null)
    //     {
    //         var translatedTitleResult = await _bookAIService.TranslatePrompt(firstBook.Book_title, "ru");
    //         var translatedTitle = translatedTitleResult.IsSuccess ? translatedTitleResult.Data.Replace("\n", "").Replace("\r", "") : firstBook.Book_title;
    //
    //         var translatedAuthorResult = await _bookAIService.TranslatePrompt(firstBook.Book_author, "ru");
    //         var translatedAuthor = translatedAuthorResult.IsSuccess ? translatedAuthorResult.Data.Replace("\n", "").Replace("\r", "") : firstBook.Book_author;
    //
    //         var translatedPublisherResult = await _bookAIService.TranslatePrompt(firstBook.Publisher, "ru");
    //         var translatedPublisher = translatedPublisherResult.IsSuccess ? translatedPublisherResult.Data.Replace("\n", "").Replace("\r", "") : firstBook.Publisher;
    //
    //         if (!await _bookRepository.GetAll().Where(w =>
    //                     w.Isbn == firstBook.Isbn || w.Publisher == firstBook.Publisher ||
    //                     w.Author == firstBook.Book_author)
    //                 .AnyAsync())
    //         {
    //             await _bookRepository.CreateAsync(new Book()
    //             {
    //                 Title = translatedTitle,
    //                 Author = translatedAuthor,
    //                 ImageUrl = firstBook.Image_url_l,
    //                 Isbn = firstBook.Isbn,
    //                 Link = firstBook.Link,
    //                 Publisher = translatedPublisher,
    //                 YearOfPublication = firstBook.Year_of_publication
    //             });
    //
    //             var result = await _bookRepository.SaveChangesAsync();
    //             if (result > 0)
    //                 return new BaseResult();
    //         }
    //
    //         return new BaseResult();
    //     }
    //
    //     return new BaseResult()
    //     {
    //         ErrorMessage = "Exception occurred while adding new books", 
    //         ErrorCode = (int)HttpStatusCode.InternalServerError
    //     };
    //
    // }
}