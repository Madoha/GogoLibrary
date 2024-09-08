using GogoLibrary.Domain.Dto.Club;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Services;

public interface IClubService
{
    Task<BaseResult> JoinToClubAsync(long clubId, string userName);
    Task<BaseResult> LeaveFromClubAsync(long clubId, string userName);
    Task<CollectionResult<ClubDto>> GetAllClubsAsync();
    Task<BaseResult> CreateClubAsync(CreateClubDto dto);
    Task<BaseResult<ClubDto>> GetClubDetailsAsync(long clubId);
}