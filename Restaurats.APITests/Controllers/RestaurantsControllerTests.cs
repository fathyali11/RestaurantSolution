using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Restaurats.Application.Common;
using Restaurats.Application.Restaurants.Dtos;
using Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
using Xunit;

namespace Restaurats.API.Controllers.Tests;

public class RestaurantsControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient=factory.CreateClient();
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
}