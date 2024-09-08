using AutoMapper;
using GogoLibrary.Domain.Dto.Club;
using GogoLibrary.Domain.Entities;

namespace GogoLibrary.Application.Mapping;

public class ClubMapping : Profile
{
    public ClubMapping()
    {
        CreateMap<Club, ClubDto>().ReverseMap();
        CreateMap<CreateClubDto, Club>().ReverseMap();
    }
}