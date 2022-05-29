using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.WebApi.Data;
using Shop.WebApi.Extensions;

namespace NSE.Identidade.API.Configuration;
public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(5, 7))));



        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddErrorDescriber<IdentityMensagensPortugues>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
