using GogoLibrary.DAL.Interceptors;
using GogoLibrary.DAL.Repositories;
using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Databases;
using GogoLibrary.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace GogoLibrary.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQl");

        services.AddSingleton<DateInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBaseRepository<UserBookRecommendation>, BaseRepository<UserBookRecommendation>>();
        services.AddScoped<IBaseRepository<Club>, BaseRepository<Club>>();
        services.AddScoped<IBaseRepository<UserClub>, BaseRepository<UserClub>>();
        services.AddScoped<IBaseRepository<BookComment>, BaseRepository<BookComment>>();
        services.AddScoped<IBaseRepository<UserBook>, BaseRepository<UserBook>>();
        services.AddScoped<IBaseRepository<UserFavoriteBook>, BaseRepository<UserFavoriteBook>>();
        services.AddScoped<IBaseRepository<Book>, BaseRepository<Book>>();
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
        services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
    }

}