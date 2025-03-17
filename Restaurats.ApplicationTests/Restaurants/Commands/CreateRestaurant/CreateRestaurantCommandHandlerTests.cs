using FluentAssertions;
using Mapster;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurats.Application.ApplicationUsers;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Interfaces;
using Restaurats.Domain.Repositories;
using Xunit;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_WhenCreateCommandIsCorrect_ShouldReturnRestaurandResponse()
    {
        RestaurantMapping.Configue();
        var unitOfWorkMock=new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var userContext=new Mock<IUserContext>();
        var restaurantAuthorizationService=new Mock<IRestaurantAuthorizationService>();

        var currentUser = new CurrentUser("i", "test@email.com", [], null, null);
        var createCommand = new CreateRestaurantCommand
        {
            City = "manasoura",
            Street = "streat one",
            PostalCode = "12-123"
        };
        var restaurant = createCommand.Adapt<Restaurant>();
        restaurant.OwnerId=currentUser.Id;


        userContext.Setup(x=>x.GetCurrentUser()).Returns(currentUser);

        unitOfWorkMock.Setup(x => x.Restaurant.AddAsync(It.IsAny<Restaurant>(), CancellationToken.None))
            .ReturnsAsync(restaurant);

        restaurantAuthorizationService.Setup(x=>x.Authorize(It.IsAny<Restaurant>(),ResourceOperation.Create))
            .Returns(true);

        var handler = new CreateRestaurantCommandHandler(unitOfWorkMock.Object,
            loggerMock.Object,
            userContext.Object,
            restaurantAuthorizationService.Object);

        var result =await handler.Handle(createCommand, CancellationToken.None);

        result.Should().NotBeNull();
        result.City.Should().Be(createCommand.City);
        result.Street.Should().Be(createCommand.Street);
        result.PostalCode.Should().Be(createCommand.PostalCode);

    }

    [Fact()]
    public void Handle_WhenNotAuthorize_ShouldThrowForebiddenException()
    {
        RestaurantMapping.Configue();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var userContext = new Mock<IUserContext>();
        var restaurantAuthorizationService = new Mock<IRestaurantAuthorizationService>();

        var currentUser = new CurrentUser("i", "test@email.com", [], null, null);
        
        userContext.Setup(x=>x.GetCurrentUser()).Returns(currentUser);

        var createCommand = new CreateRestaurantCommand
        {
            City = "manasoura",
            Street = "streat one",
            PostalCode = "12-123"
        };
        var restaurant = createCommand.Adapt<Restaurant>();
        restaurant.OwnerId = currentUser.Id;

        restaurantAuthorizationService.Setup(x => x.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update))
            .Returns(false);

        var handler = new CreateRestaurantCommandHandler(unitOfWorkMock.Object,
            loggerMock.Object,
            userContext.Object,
            restaurantAuthorizationService.Object);

        var exception= ()=> handler.Handle(createCommand,CancellationToken.None);

        exception
            .Should()
            .ThrowAsync<ForbiddenException>();
    }
}
