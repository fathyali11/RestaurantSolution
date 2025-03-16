using FluentAssertions;
using Mapster;
using Restaurats.API.UpdateRestaurant;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Domain.Entities;
using Xunit;

namespace Restaurats.Application.Restaurants.Dtos.Tests;

public class RestaurantMappingTests
{
    public RestaurantMappingTests()
    {
        RestaurantMapping.Configue();
    }
    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantWithDishesResponse_ShoulMapsCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Name = "Test Restaurant",
            Address = new Address
            {
                City = "Cairo",
                Street = "Main St",
                PostalCode = "12345"
            },
            Dishes = new List<Dish>
            {
                new Dish { Id = 1, Name = "Pizza", Price = 10 },
                new Dish { Id = 2, Name = "Pasta", Price = 15 }
            }
        };

        var response=restaurant.Adapt<RestaurantWithDishesResponse>();

        response.Should().NotBeNull();
        response.City.Should().Be(restaurant.Address.City);
        response.Street.Should().Be(restaurant.Address.Street);
        response.PostalCode.Should().Be(restaurant.Address.PostalCode);
        response.Dishes.Should().NotBeNull().And.HaveCount(2);
        response.Dishes.Should().Contain(d => d.Name == "Pizza" && d.Price == 10);
        response.Dishes.Should().Contain(d => d.Name == "Pasta" && d.Price == 15);
    }

    [Fact()]
    public void CreateMap_ForUpdateRestaurantCommandToRestaurant_ShoulMapsCorrectly()
    {
        // Arrange
        var updateCommand = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "update command test",
            Description = "it descripe update command",
            HasDelivery = true
        };

        var restaurant = updateCommand.Adapt<Restaurant>();

        restaurant.Should().NotBeNull();
        restaurant.Id.Should().Be(updateCommand.Id);
        restaurant.Name.Should().Be(updateCommand.Name);
        restaurant.Description.Should().Be(updateCommand.Description);
        restaurant.HasDelivery.Should().BeTrue();
    }
    [Fact()]
    public void CreateMap_ForCreateRestaurantCommandToRestaurant_ShoulMapsCorrectly()
    {
        // Arrange
        var createCommand = new CreateRestaurantCommand
        {
            City="manasoura",
            Street="streat one",
            PostalCode="12-123"
        };

        var restaurant = createCommand.Adapt<Restaurant>();

        restaurant.Should().NotBeNull();
        restaurant.Address!.City.Should().Be(createCommand.City);
        restaurant.Address.Street.Should().Be(createCommand.Street);
        restaurant.Address.PostalCode.Should().Be(createCommand.PostalCode);
    }

    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantResponse_ShoulMapsCorrectly()
    {
        // Arrange
        var restaurant = new Restaurant
        {
            Address = new Address
            {
                City = "Cairo",
                Street = "Main St",
                PostalCode = "12345"
            }
        };

        var response = restaurant.Adapt<RestaurantResponse>();

        response.Should().NotBeNull();
        response.City.Should().Be(restaurant.Address.City);
        response.PostalCode.Should().Be(restaurant.Address.PostalCode);
        response.Street.Should().Be(restaurant.Address.Street);
    }
}