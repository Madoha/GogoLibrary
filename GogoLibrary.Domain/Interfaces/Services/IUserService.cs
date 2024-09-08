using GogoLibrary.Domain.Dto.Book;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IUserService
{
    Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string userName);
    Task<BaseResult> AddToFavoritesAsync(string userName, long bookId);
    Task<BaseResult> RecommendBookAsync(string userName, long bookId);
    Task<CollectionResult<SearchBookResultDto>> GetFavoritesAsync(string userName);
}