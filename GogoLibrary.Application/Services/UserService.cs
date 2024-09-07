using System.Net;
using System.Security.Claims;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public UserService(IBaseRepository<User> userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string userName)
    {
        var user = await _userRepository.GetAll().FirstOrDefaultAsync(w => w.UserName == userName);
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
}