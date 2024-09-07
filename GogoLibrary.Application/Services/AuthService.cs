using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Dto.Token;
using GogoLibrary.Domain.Dto.User;
using GogoLibrary.Domain.Enum;
using GogoLibrary.Domain.Interfaces.Databases;
using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Interfaces.Services;
using GogoLibrary.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GogoLibrary.Application.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<UserToken> _userTokenRepository;
    private readonly IBaseRepository<Role> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ITokenService _tokenService;

    public AuthService(IBaseRepository<User> userRepository, 
        IBaseRepository<UserToken> userTokenRepository, 
        ITokenService tokenService, 
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        ILogger logger, 
        IBaseRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _userTokenRepository = userTokenRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _roleRepository = roleRepository;
    }

    public async Task<BaseResult<TokenDto>> LoginUserAsync(LoginUserDto dto)
    {
        var user = await _userRepository.GetAll().Include(x => x.Roles).FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user == null)
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = "User does not exist",
                ErrorCode = (int)HttpStatusCode.NotFound
            };
        }

        if (!IsVerifyPassword(user.Password, dto.Password))
        {
            return new BaseResult<TokenDto>()
            {
                ErrorMessage = "Password is not correct",
                ErrorCode = (int)HttpStatusCode.BadRequest
            };
        }
        var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(u => u.UserId == user.Id);

        var userRoles = user.Roles;
        var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
        claims.Add(new Claim(ClaimTypes.Name, dto.UserName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));

        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        if (userToken == null)
        {
            userToken = new UserToken()
            {
                UserId = user.Id,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };
            await _userTokenRepository.CreateAsync(userToken);
            await _userTokenRepository.SaveChangesAsync();
        }
        else
        {
            userToken.RefreshToken = refreshToken;
            userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userTokenRepository.UpdateAsync(userToken);
            await _userTokenRepository.SaveChangesAsync();
        }
        return new BaseResult<TokenDto>()
        {
            Data = new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        };
    }

    public async Task<BaseResult<UserDto>> RegisterUserAsync(RegisterUserDto dto)
    {
        if (dto.Password != dto.ConfirmPassword)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = "Passwords do not match",
                ErrorCode = (int)HttpStatusCode.BadRequest
            };
        }

        var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user != null)
        {
            return new BaseResult<UserDto>()
            {
                ErrorMessage = "User already exists",
                ErrorCode = (int)HttpStatusCode.BadRequest
            };
        }

        var hashUserPassword = HashPasswod(dto.Password);

        using (var transaction = await _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                user = new User()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    UserName = dto.UserName,
                    Password = hashUserPassword,
                    PhoneNumber = dto.PhoneNumber
                };
                await _unitOfWork.Users.CreateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == nameof(Roles.User));
                if (role == null)
                {
                    return new BaseResult<UserDto>()
                    {
                        ErrorMessage = "Role not found",
                        ErrorCode = (int)HttpStatusCode.NotFound
                    };
                }
                UserRole userRole = new UserRole()
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                await _unitOfWork.UserRoles.CreateAsync(userRole);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        return new BaseResult<UserDto>()
        {
            Data = _mapper.Map<UserDto>(user),
        };
    }
    private string HashPasswod(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool IsVerifyPassword(string userPasswordHash, string userPassword)
    {
        var hash = HashPasswod(userPassword);
        return userPasswordHash == hash;
    }
}