using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restuarants.infrastructure.Authorization;
using Restuarants.infrastructure.Authorization.Policies;
using Restuarants.infrastructure.Authorization.Policies.Have2Restaurants;
using Restuarants.infrastructure.Authorization.Services;
using Restuarants.infrastructure.Configrutions;
using Restuarants.infrastructure.Persistence;
using Restuarants.infrastructure.Repositories;
using Restuarants.infrastructure.Seeders;
using Restuarants.infrastructure.Storage.Blob;

namespace Restuarants.infrastructure.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMainDb(configuration);
        services.AddRepositories();
        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();
        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                policy => policy.RequireClaim(ClaimsTypes.Nationality))
            .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(
                    new MinimumAgeRequirement(20)
                ))
            .AddPolicy(PolicyNames.AtLeast2Restaurant,
                policy => policy.Requirements.Add(new Have2Restaurants())
            );
        services.Configure<BlobStorageSetting>(configuration.GetSection("BlobStorage"));
    }

    public static async void Seed(this IServiceProvider services)
    {
        var scope = services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
        await seeder.Seed();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, Have2RestaurantsHandler>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }

    private static void AddMainDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
        );
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    }
}