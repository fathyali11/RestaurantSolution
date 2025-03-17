using System.Linq.Expressions;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurats.Application.ApplicationUsers;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Exceptions;
using Restaurats.Domain.Interfaces;
using Restaurats.Domain.Repositories;
using Xunit;

namespace Restaurats.API.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_WhenUpdateCommandIsValid_ShoulReturnResponse()
    {
        RestaurantMapping.Configue();

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        var userContextMock = new Mock<IUserContext>();
        var mapperMock = new Mock<IMapper>();
        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        var currentUser = new CurrentUser("i", "test@email.com", [], null, null);
        var updateCommand = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "test",
            Description = "test",
            HasDelivery = true
        };

        var restaurant=updateCommand.Adapt<Restaurant>();

        unitOfWorkMock.Setup(x => x.Restaurant.GetByIdAsync(It.IsAny<Expression<Func<Restaurant, bool>>>(), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(restaurant);


        restaurantAuthorizationServiceMock.Setup(x => x.Authorize(It.IsAny<Restaurant>(), ResourceOperation.Update))
            .Returns(true);

        mapperMock.Setup(x=>x.Map(updateCommand,restaurant))
            .Callback(()=>
            {
                restaurant.Name = updateCommand.Name;
                restaurant.Description = updateCommand.Description;
                restaurant.HasDelivery = updateCommand.HasDelivery;
            }
            );

        var handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object,
            mapperMock.Object,
            loggerMock.Object,
            restaurantAuthorizationServiceMock.Object
            );


        await handler.Handle(updateCommand, CancellationToken.None);

        unitOfWorkMock.Verify(x=> x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        mapperMock.Verify(x=>x.Map(updateCommand,restaurant), Times.Once());


    }

    [Fact()]
    public void Handle_WhenUpdateRestaurantCommandAndRestaurantDonnotExistInDatabase_ShouldThrowNotFoundException()
    {

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        var userContextMock = new Mock<IUserContext>();
        var mapperMock = new Mock<IMapper>();
        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        unitOfWorkMock.Setup(x => x.Restaurant.GetByIdAsync(It.IsAny<Expression<Func<Restaurant, bool>>>(), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Restaurant?)null);
        var updateCommand = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "test",
            Description = "test",
            HasDelivery = true
        };

        var handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object,
            mapperMock.Object,
            loggerMock.Object,
            restaurantAuthorizationServiceMock.Object
            );

        var exception = () => handler.Handle(updateCommand, CancellationToken.None);

        exception
            .Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage(nameof(Restaurant), updateCommand.Id.ToString());
    }

    [Fact()]
    public void Handle_WhenUpdateRestaurantCommandAndRestaurantUserNotAuthorize_ShouldThrowForbiddenException()
    {

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        var userContextMock = new Mock<IUserContext>();
        var mapperMock = new Mock<IMapper>();
        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();


        unitOfWorkMock.Setup(x => x.Restaurant.GetByIdAsync(It.IsAny<Expression<Func<Restaurant, bool>>>(), null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Restaurant { Id=1,Name="test",Description="test"});
        restaurantAuthorizationServiceMock.Setup(x=>x.Authorize(It.IsAny<Restaurant>(),It.IsAny<ResourceOperation>()))
            .Returns(false);

        var updateCommand = new UpdateRestaurantCommand
        {
            Id = 1,
            Name = "test",
            Description = "test",
            HasDelivery = true
        };

        var handler = new UpdateRestaurantCommandHandler(unitOfWorkMock.Object,
            mapperMock.Object,
            loggerMock.Object,
            restaurantAuthorizationServiceMock.Object
            );

        var exception = () => handler.Handle(updateCommand, CancellationToken.None);

        exception
            .Should()
            .ThrowAsync<ForbiddenException>();
    }
}