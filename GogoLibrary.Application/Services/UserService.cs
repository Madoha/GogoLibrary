using System.Net;
using System.Security.Claims;
using AutoMapper;
using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace GogoLibrary.Application.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserFavoriteBook> _userFavoriteBookRepository;
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<UserBookRecommendation> _userBookRecommendationRepository;
    private readonly IMapper _mapper;

    public UserService(IBaseRepository<User> userRepository,
        IMapper mapper, 
        IBaseRepository<UserFavoriteBook> userFavoriteBookRepository, 
        IBaseRepository<Book> bookRepository, 
        IBaseRepository<UserBookRecommendation> userBookRecommendationRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userFavoriteBookRepository = userFavoriteBookRepository;
        _bookRepository = bookRepository;
        _userBookRecommendationRepository = userBookRecommendationRepository;
    }

    public async Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string userName)
    {
        var user = await FindUserAsync(userName);
        if (user != null)
        {
            return new BaseResult<UserProfileDto>()
            {
                Data = _mapper.Map<UserProfileDto>(user)
            };
        }

        return new BaseResult<UserProfileDto>()
        {
            ErrorMessage = "User does not exist",
            ErrorCode = (int)HttpStatusCode.NotFound,
        };
    }

    public async Task<BaseResult> AddToFavoritesAsync(string userName, long bookId)
    {
        var user = await FindUserAsync(userName);
        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = "User does not exist",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }
        
        var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == bookId);
        if (book == null)
        {
            return new BaseResult()
            {
                ErrorMessage = "Book does not exist",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }

        var favoriteBook = new UserFavoriteBook()
        {
            UserId = user.Id,
            BookId = book.Id,
        };

        await _userFavoriteBookRepository.CreateAsync(favoriteBook);
        await _userFavoriteBookRepository.SaveChangesAsync();

        return new BaseResult();
    }

    public async Task<BaseResult> RecommendBookAsync(string userName, long bookId)
    {
        var user = await FindUserAsync(userName);
        var book = await _bookRepository.GetAll().FirstOrDefaultAsync(x => x.Id == bookId);
        if (user == null || book == null)
        {
            return new BaseResult()
            {
                ErrorMessage = "User or book does not exist",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }

        var recommendationBook = new UserBookRecommendation()
        {
            UserId = user.Id,
            BookId = book.Id
        };

        await _userBookRecommendationRepository.CreateAsync(recommendationBook);
        await _userBookRecommendationRepository.SaveChangesAsync();
        
        return new BaseResult();
    }

    public async Task<CollectionResult<SearchBookResultDto>> GetFavoritesAsync(string userName)
    {
        var user = await FindUserAsync(userName);

        var favoriteBooks = await _userFavoriteBookRepository
            .GetAll()
            .Where(w => w.UserId == user.Id)
            .ToListAsync();

        var favoriteBookIds = favoriteBooks.Select(fb => fb.BookId).ToList();

        var books = await _bookRepository
            .GetAll()
            .Where(w => favoriteBookIds.Contains(w.Id))
            .ToListAsync();

        if (books == null || books.Count == 0)
        {
            return new CollectionResult<SearchBookResultDto>()
            {
                ErrorMessage = "Вы не добавляли в книжную полку книги",
                ErrorCode = (int)HttpStatusCode.NotFound,
            };
        }

        return new CollectionResult<SearchBookResultDto>()
        {
            Data = _mapper.Map<List<SearchBookResultDto>>(books),
            Count = books.Count,
        };
    }

    private async Task<User> FindUserAsync(string userName)
    {
        return await _userRepository.GetAll().FirstOrDefaultAsync(w => w.UserName == userName);
    }
}