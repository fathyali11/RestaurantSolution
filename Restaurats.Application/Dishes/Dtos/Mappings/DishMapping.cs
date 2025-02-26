using Mapster;
using Restaurats.Domain.Entities;

namespace Restaurats.Application.Dishes.Dtos.Mappings;
internal class DishMapping
{
    public static void Configure()
    {
        TypeAdapterConfig<Dish, DishResponse>.NewConfig();
        TypeAdapterConfig<CreateDishRequest, Dish>.NewConfig();
    }
}
