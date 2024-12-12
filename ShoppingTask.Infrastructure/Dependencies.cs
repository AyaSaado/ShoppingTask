global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using ShoppingTask.Core.Models;
global using ShoppingTask.Infrastructure.Data;
global using ShoppingTask.Infrastructure.Repository.Base;
global using ShoppingTask.Infrastructure.Repository.UnitOfWork;
global using ShoppingTask.Infrastructure.Repository.Users;

namespace ShoppingTask.Infrastructure;

public static class Dependencies
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        bool isProduction =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";

        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDbContext<AppDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("Default"));
                if (!isProduction)
                {
                    o.EnableSensitiveDataLogging();
                }
            })
            .AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(op =>
            op.TokenLifespan = TimeSpan.FromHours(1)
        );
    }
}
