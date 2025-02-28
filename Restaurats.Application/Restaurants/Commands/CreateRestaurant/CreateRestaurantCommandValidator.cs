using FluentValidation;
using Restaurats.Application.Dishes.Commands.CreateDish;
namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category cannot exceed 50 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal Code is required.")
            .Matches(@"^\d{5}(-\d{4})?$").WithMessage("Invalid Postal Code format.");

        RuleFor(x => x.LogoSasUrl)
            .NotEmpty().WithMessage("Logo URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Invalid Logo URL format.");

        RuleFor(x => x.Dishes)
            .NotNull().WithMessage("Dishes list cannot be null.")
            .Must(dishes => dishes.Any()).WithMessage("At least one dish must be provided.");

        RuleForEach(x => x.Dishes)
            .SetValidator(new CreateDishCommandValidator());
    }
}
