using System.Net;
using AutoMapper;
using GogoLibrary.Domain.Dto.Club;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace GogoLibrary.Application.Services;

public class ClubService : IClubService
{
    private readonly IBaseRepository<Club> _clubRepository;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserClub> _userClubRepository;
    private readonly IMapper _mapper;

    public ClubService(IBaseRepository<Club> clubRepository, 
        IBaseRepository<User> userRepository, 
        IBaseRepository<UserClub> userClubRepository, 
        IMapper mapper)
    {
        _clubRepository = clubRepository;
        _userRepository = userRepository;
        _userClubRepository = userClubRepository;
        _mapper = mapper;
    }

    public async Task<BaseResult> JoinToClubAsync(long clubId, string userName)
    {
        var club = await _clubRepository.GetAll().Where(x => x.Id == clubId).FirstOrDefaultAsync();
        var user = await GetUserByNameAsync(userName);
        if (user.IsSuccess)
        {
            if (club == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = "The club does not exists",
                    ErrorCode = (int)HttpStatusCode.NotFound,
                };
            }
            
            var userClub = await _userClubRepository.GetAll().Where(w => w.UserId == user.Data.Id && w.ClubId == clubId).FirstOrDefaultAsync();
            if (userClub != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = "Ты уже в этом клане",
                };
            }

            userClub = new UserClub()
            {
                ClubId = clubId,
                UserId = user.Data.Id
            };

            try
            {
                await _userClubRepository.CreateAsync(userClub);
                await _userClubRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return new BaseResult();
    }

    public async Task<BaseResult> LeaveFromClubAsync(long clubId, string userName)
    {
        try
        {
            var user = await GetUserByNameAsync(userName);
            var club = await _clubRepository.GetAll().Where(x => x.Id == clubId).FirstOrDefaultAsync();
            var userClub = await _userClubRepository.GetAll().Where(w => w.ClubId == clubId && w.UserId == user.Data.Id)
                .FirstOrDefaultAsync();
            _userClubRepository.DeleteAsync(userClub);
            await _userClubRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        return new BaseResult();
    }

    public async Task<CollectionResult<ClubDto>> GetAllClubsAsync()
    {
        var clubs = await _clubRepository.GetAll().ToListAsync();
        return new CollectionResult<ClubDto>()
        {
            Data = _mapper.Map<List<ClubDto>>(clubs),
            Count = clubs.Count
        };
    }

    public async Task<BaseResult> CreateClubAsync(CreateClubDto dto)
    {
        var club = await _clubRepository.GetAll().Where(x => x.Name == dto.Name).FirstOrDefaultAsync();
        if (club != null)
        {
            return new BaseResult()
            {
                ErrorMessage = "Club already exists",
                ErrorCode = (int)HttpStatusCode.BadRequest,
            };
        }
        
        var clubEntity = _mapper.Map<Club>(dto);
        await _clubRepository.CreateAsync(clubEntity);
        await _clubRepository.SaveChangesAsync();
        
        return new BaseResult();
    }

    public async Task<BaseResult<ClubDto>> GetClubDetailsAsync(long clubId)
    {
        var club = await _clubRepository.GetAll().Where(c => c.Id == clubId).FirstOrDefaultAsync();
        return new BaseResult<ClubDto>()
        {
            Data = _mapper.Map<ClubDto>(club),
        };
    }

    private async Task<BaseResult<User>> GetUserByNameAsync(string userName)
    {
        var user = await _userRepository.GetAll().Where(x => x.UserName == userName).FirstOrDefaultAsync();
        if (user != null)
        {
            return new BaseResult<User>()
            {
                Data = user
            };
        }

        return new BaseResult<User>();
    }
}