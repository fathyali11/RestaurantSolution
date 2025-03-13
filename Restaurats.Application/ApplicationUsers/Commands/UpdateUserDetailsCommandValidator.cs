using FluentValidation;

namespace Restaurats.Application.ApplicationUsers.Commands;
internal class UpdateUserDetailsCommandValidator: AbstractValidator<UpdateUserDetailsCommand>
{
    public UpdateUserDetailsCommandValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of Birth must be in the past.");

        RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("Nationality is required.")
            .MaximumLength(100).WithMessage("Nationality must not exceed 100 characters.");
    }
}
