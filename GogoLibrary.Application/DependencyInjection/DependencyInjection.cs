using GogoLibrary.Application.Mapping;
using GogoLibrary.Application.Services;
using GogoLibrary.DAL.Repositories;
using GogoLibrary.Domain.Interfaces.Databases;
using GogoLibrary.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GogoLibrary.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserMapping));
        
        services.InitServices();
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ICommentServices, CommentService>();
    }
}