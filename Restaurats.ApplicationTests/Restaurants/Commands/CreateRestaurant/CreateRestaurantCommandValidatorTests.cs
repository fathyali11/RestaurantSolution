using System.Xml.Linq;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Restaurats.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void ValidateName_WhenNameIsApplyAllRoles_ShouldNotGiveAnyError()
    {
        var validator=new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Theory()]
    [InlineData("ff")]
    [InlineData("")]
    public void ValidateName_WhenNameIsLessThan3Chars_ShouldGiveError(string name)
    {
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = name,
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name must be between 3 and 100 characters.");
    }

    [Fact()]
    public void ValidateName_WhenNameIsMoreThan100Chars_ShouldGiveError()
    {
        var stringMoreThan100Chars = new string('a', 101);
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = stringMoreThan100Chars,
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Name must be between 3 and 100 characters.");
    }

    [Fact()]
    public void ValidateCategory_WhenCategoryContainValidCategories_ShouldGiveNotAnyErrors()
    {
        List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

        var validator=new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Category);

    }
    [Fact()]
    public void ValidateCategory_WhenCategoryNotContainValidCategories_ShouldGiveErrors()
    {
        List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Category)
            .WithErrorMessage("Invalid category. Please choose from the valid categories.");

    }


    [Fact]
    public void ValidateContactEmail_WhenItIsValid_ShouldNotGiveErrors()
    {
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.ContactEmail);
    }
    [Theory]
    [InlineData("testGmail.com")]
    public void ValidateContactEmail_WhenItIsNotValid_ShouldGiveErrors(string email)
    {
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "test",
            HasDelivery = true,
            ContactEmail = email,
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12345"
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.ContactEmail)
            .WithErrorMessage("Please provide a valid email address");
    }




    [Fact]
    public void ValidatePostalCode_WhenItIsValid_ShouldNotGiveErrors()
    {
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = "12-345"
        };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.PostalCode);
    }

    [Theory]
    [InlineData("12344")]
    [InlineData("123-44")]
    [InlineData("12fds344")]
    public void ValidatePostalCode_WhenItIsNotValid_ShouldGiveErrors(string postalCode)
    {
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "A test restaurant description",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "test@example.com",
            ContactNumber = "123-456-7890",
            City = "Test City",
            Street = "123 Test St",
            PostalCode = postalCode
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.PostalCode)
            .WithErrorMessage("Please provide a valid postal code (XX-XXX).");
    }
}
