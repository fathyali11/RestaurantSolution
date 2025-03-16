using FluentAssertions;
using Restaurats.Domain.Constants;
using Xunit;

namespace Restaurats.Application.ApplicationUsers.Tests;

public class CurrentUserTests
{
    [Theory()]
    [InlineData(UserRoles.OwnerRole)]
    [InlineData(UserRoles.AdminRole)]
    [InlineData(UserRoles.UserRole)]
    public void IsInRole_WhenRoleIsExist_ShouldReturnTrue(string role)
    {
        // arrange
        var roles = new List<string> { UserRoles.AdminRole, UserRoles.OwnerRole, UserRoles.UserRole };
        var currentUser = new CurrentUser("1", "test@gmail.com", roles, null, null);
        //act
        var isInRole=currentUser.IsInRole(role);
        //assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRole_WhenRoleIsNotExist_ShouldReturnFalse()
    {
        // arrange
        var roles = new List<string> { UserRoles.AdminRole, UserRoles.UserRole };
        var currentUser = new CurrentUser("1", "test@gmail.com", roles, null, null);
        //act
        var isInRole = currentUser.IsInRole(UserRoles.OwnerRole);
        //assert
        isInRole.Should().BeFalse();
    }

}