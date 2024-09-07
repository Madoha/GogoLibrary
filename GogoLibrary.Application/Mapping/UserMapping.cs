using AutoMapper;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Entities;

namespace GogoLibrary.Application.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<UserProfileDto, User>().ReverseMap();
    }
}