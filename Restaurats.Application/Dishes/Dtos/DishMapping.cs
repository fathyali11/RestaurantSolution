using Mapster;
using Restaurats.Application.Dishes.Commands.CreateDish;
using Restaurats.Domain.Entities;

namespace Restaurats.Application.Dishes.Dtos;
internal class DishMapping
{
    public static void Configure()
    {
        TypeAdapterConfig<Dish, DishResponse>.NewConfig();
        TypeAdapterConfig<CreateDishCommand, Dish>.NewConfig();
    }
}
