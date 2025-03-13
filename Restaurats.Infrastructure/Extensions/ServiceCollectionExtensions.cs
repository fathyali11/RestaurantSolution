using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurats.Domain;
using Restaurats.Domain.Repositories;
using Restaurats.Infrastructure.Presistence;
using Restaurats.Infrastructure.Repositories;
using Restaurats.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Restaurats.Domain.Entities;

namespace Restaurats.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RestaurantDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("RestaurantConnection"));
        });


        services.AddScoped<IRestaurantSeeder,RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository,RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();

        services.AddIdentityApiEndpoints<ApplicationUser>()
            .AddEntityFrameworkStores<RestaurantDbContext>();


        return services;
    }
}
