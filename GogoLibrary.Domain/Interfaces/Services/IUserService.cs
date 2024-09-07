using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IUserService
{
    Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string userName);
}