using FluentValidation.TestHelper;
using Restaurats.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurats.Application.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact]
    public void Validator_ForValidCommand_ShouldNotReturnErrors()
    {
        // Arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "test",
            Description = "its a test",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@da"
        };

        // Act
        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();

    }
    [Theory]
    [InlineData("", "InvalidCategory", "invalid-email", "12345")] 
    [InlineData("A", "French", "test", "12-3456")]
    [InlineData("AB", "Thai", "test.com", "AA-BBB")] 
    [InlineData(" ", "", "test.com", "99-9999")] 
    [InlineData("Th", "Greek", "example.com", "A2-34A")]
    [InlineData("Hi", "RandomFood", "missing", "0000-0")]
    [InlineData("Re", "Unknown", "justtext", "1-2345")]
    [InlineData("", "", "", "")] 
    public void Validator_ForValidCommand_ShouldReturnErrors(string name,string category,string contactEmail,string postalCode)
    {
        // Arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand(
            );

        // Act
        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name must be between 3 and 100 characters.");

        result.ShouldHaveValidationErrorFor(x => x.Category)
            .WithErrorMessage("Invalid category. Please choose from the valid categories.");

        result.ShouldHaveValidationErrorFor(x => x.ContactEmail)
            .WithErrorMessage("Please provide a valid email address");

        result.ShouldHaveValidationErrorFor(x => x.PostalCode)
            .WithErrorMessage("Please provide a valid postal code (XX-XXX).");

    }

}
