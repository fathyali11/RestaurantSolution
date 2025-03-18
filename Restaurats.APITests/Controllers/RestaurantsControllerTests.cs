using Restaurats.API.Controllers;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Restaurats.APITests;
using Restaurats.Application.Common;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
using Xunit;
using Azure.Core;
using Restaurats.Domain.Entities;
using Restaurats.Domain.Repositories;
using System.Linq.Expressions;

namespace Restaurats.API.Controllers.Tests;

public class RestaurantsControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient =
        factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            });
        }).CreateClient();
    [Fact()]
    public async Task GetAll_WhenValidQuery_ShouldReturnStatusCode200()
    {
        var response = await _httpClient.GetAsync($"/api/Restaurants?SearchTerm=te&SortBy=name&OrderDirection=asc&PageNumber=1&PageSize=10");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact()]
    public async Task GetAll_WhenValidNotQuery_ShouldReturnStatusCode404()
    {
        var response = await _httpClient.GetAsync($"/api/Restaurants?PageNumber=1&PageSize=10");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }

    [Fact()]
    public async Task Get_WhenIdIsValid_ShouldReturnStatusCode200()
    {
        var response=await _httpClient.GetAsync("/api/Restaurants/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact()]
    public async Task Get_WhenIdIsNotValid_ShouldReturnStatusCode200()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.Setup(x=>x.Restaurant.GetByIdAsync(It.IsAny<Expression<Func<Restaurant, bool>>>(),null, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Restaurant?)null);

        var response = await _httpClient.GetAsync("/api/Restaurants/0");
        var content= await response.Content.ReadAsStringAsync();
        content.Should().Be($"{nameof(Restaurant)} with id: 0 doesn't exist");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}