using Microsoft.Extensions.DependencyInjection;
using Mapster;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Application.Dishes.Dtos;
using FluentValidation;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurats.Application.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssemply=typeof(ServiceCollectionExtension).Assembly;

        services.AddMapster();
        RestaurantMapping.Configue();
        DishMapping.Configure();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(applicationAssemply);
        });
        services.AddValidatorsFromAssemblyContaining<CreateRestaurantCommandValidator>();

        return services;
    }
}
