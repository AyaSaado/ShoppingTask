global using ShoppingTask.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using ShoppingTask.Core.Session;

namespace ShoppingTask.Core;

public static class Dependencies
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services.AddSingleton<ShoppingCart>().AddAutoMapper(typeof(Profiler).Assembly);
    }
}
