using FluentValidation;

namespace Restaurats.Application.Dishes.Commands.CreateDish;
internal class CreateDishCommandValidator: AbstractValidator<CreateDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Dish name is required.")
            .MaximumLength(100).WithMessage("Dish name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Dish description is required.")
            .MaximumLength(300).WithMessage("Dish description cannot exceed 300 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.KiloCalories)
            .GreaterThan(0).WithMessage("KiloCalories must be greater than zero.");

        RuleFor(x => x.RestaurantId)
            .GreaterThan(0).WithMessage("RestaurantId must be greater than zero.");
    }
}
