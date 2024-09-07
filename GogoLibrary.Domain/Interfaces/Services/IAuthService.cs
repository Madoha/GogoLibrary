using GogoLibrary.Domain.Dto.Token;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<BaseResult<UserDto>> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<BaseResult<TokenDto>> LoginUserAsync(LoginUserDto loginUserDto);
}