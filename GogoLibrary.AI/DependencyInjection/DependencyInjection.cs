using GogoLibrary.AI.Services;
using GogoLibrary.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GogoLibrary.AI.DependencyInjection;

public static class DependencyInjection
{
    public static void AddAILayer(this IServiceCollection services)
    {
        services.AddHttpClient<IBookAIService, BookAIService>();
    }
}