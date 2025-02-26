using Microsoft.Extensions.DependencyInjection;
using Restaurats.Application.Restaurants.Services;
using Mapster;
using Restaurats.Application.Restaurants.Dtos.Mappings;
using Restaurats.Application.Dishes.Dtos.Mappings;

namespace Restaurats.Application.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssemply=typeof(ServiceCollectionExtension).Assembly;

        services.AddMapster();
        RestaurantMapping.Configue();
        DishMapping.Configure();

        services.AddScoped<IRestaurantService, RestaurantService>();
        return services;
    }
}
