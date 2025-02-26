using Mapster;
using Restaurats.Application.Dishes.Dtos;
using Restaurats.Domain.Entities;

namespace Restaurats.Application.Restaurants.Dtos.Mappings;
internal class RestaurantMapping
{
    public static void Configue()
    {
        TypeAdapterConfig<Restaurant, RestaurantResponse>.NewConfig()
            .Map(dest => dest.City, src => src.Address!.City)
            .Map(dest => dest.Street, src => src.Address!.Street)
            .Map(dest => dest.PostalCode, src => src.Address!.PostalCode)
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<DishResponse>>());


        TypeAdapterConfig<CreateRestaurantRequest, Restaurant>.NewConfig()
            .Map(dest => dest.Address, src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            })
            .Map(dest => dest.Dishes, src => src.Dishes.Adapt<List<Dish>>());

        
        

    }
}
