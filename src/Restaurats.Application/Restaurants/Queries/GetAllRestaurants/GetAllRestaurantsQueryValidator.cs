using FluentValidation;
using Restaurats.Domain.Entities;

namespace Restaurats.Application.Restaurants.Queries.GetAllRestaurants;
internal class GetAllRestaurantsQueryValidator: AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly string[] _allowedSortByColumn = { nameof(Restaurant.Id), nameof(Restaurant.Name), nameof(Restaurant.Description) };
    private readonly string[] _allowedOrderDirection = { "asc", "desc" };
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.SearchTerm)
            .NotEmpty().WithMessage("Search term is required");

        RuleFor(x => x.SortBy)
            .Must(x => _allowedSortByColumn.Contains(x.ToLower()))
            .WithMessage($"Invalid sort by column allowed colums is {string.Join(",", _allowedSortByColumn)}");


        RuleFor(x => x.OrderDirection)
            .Must(x => _allowedOrderDirection.Contains(x.ToLower()))
            .WithMessage($"Invalid order by allowed values is {string.Join(",", _allowedOrderDirection)}");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0");
    }
}
