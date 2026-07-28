using DeliverySystem.Infrastructure.Persistence;
using DeliverySystem.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliverySystem.Infrastructure;

public static class DependenceInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(cfg =>
        {
            cfg.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
        {
            cfg.Password.RequiredLength = 6;
            cfg.User.RequireUniqueEmail = true;
            cfg.Lockout.AllowedForNewUsers = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}