using FluentValidation;

namespace Restaurats.API.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Restaurant name is required.")
            .MaximumLength(100).WithMessage("Restaurant name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.HasDelivery)
            .NotNull().WithMessage("HasDelivery is required.");
    }
}
