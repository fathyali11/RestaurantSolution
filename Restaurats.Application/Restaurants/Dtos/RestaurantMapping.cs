using Mapster;
using Restaurats.API.UpdateRestaurant;
using Restaurats.Application.Dishes.Dtos;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Domain.Entities;

namespace Restaurats.Application.Restaurants.Dtos;
public class RestaurantMapping
{
    public static void Configue()
    {
        TypeAdapterConfig<Restaurant, RestaurantWithDishesResponse>.NewConfig()
            .Map(dest => dest.City, src => src.Address!.City)
            .Map(dest => dest.Street, src => src.Address!.Street)
            .Map(dest => dest.PostalCode, src => src.Address!.PostalCode)
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<DishResponse>>());

        TypeAdapterConfig<Restaurant, RestaurantResponse>.NewConfig()
            .Map(dest => dest.City, src => src.Address!.City)
            .Map(dest => dest.Street, src => src.Address!.Street)
            .Map(dest => dest.PostalCode, src => src.Address!.PostalCode);

        TypeAdapterConfig<CreateRestaurantCommand, Restaurant>.NewConfig()
            .Map(dest => dest.Address, src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            });


        TypeAdapterConfig<UpdateRestaurantCommand, Restaurant>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.HasDelivery, src => src.HasDelivery);

    }
}
