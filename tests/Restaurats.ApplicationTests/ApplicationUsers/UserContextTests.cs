using Xunit;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using FluentAssertions;

namespace Restaurats.Application.ApplicationUsers.Tests;

public class UserContextTests
{

    [Fact]  
    public void GetCurrentUser_WhenUserIsAuthenticated_ShouldReturnCurrentUser()
    {
        // arrange
        var httpContextAccesor = new Mock<IHttpContextAccessor>();
        var userContext = new UserContext(httpContextAccesor.Object);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@example.com"),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("nationality", "USA"),
            new Claim("dateOfBirth", "1990-01-01")
        };
        
        var claimsPrincipal=new ClaimsPrincipal(new ClaimsIdentity(claims,"Tests"));

        httpContextAccesor.Setup(x=>x.HttpContext).Returns(new DefaultHttpContext()
        {
            User=claimsPrincipal
        });

        var currentUser=userContext.GetCurrentUser();

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be(currentUser.Id);
        currentUser.Email.Should().Be(currentUser.Email);
        currentUser.Roles.Should().ContainInOrder(currentUser.Roles);
        currentUser.Nationality.Should().Be(currentUser.Nationality);
        currentUser.Date.Should().Be(currentUser.Date);

    }

    [Fact]
    public void GetCurrentUser_WhenContextNotFound_ShouldThrowInvalidOperationException()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        
        var userContext = new UserContext(httpContextAccessor.Object);
        httpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var exception= ()=>userContext.GetCurrentUser();

        exception
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("user context is not found");
    }

    [Fact]
    public void GetCurrentUser_WhenUserNotAuthenticated_ShouldReturnNull()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        var context = new DefaultHttpContext
        {
            User = user
        };


        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var userContext = new UserContext(httpContextAccessor.Object);
        httpContextAccessor.Setup(x => x.HttpContext).Returns(context);

        var nullUser = userContext.GetCurrentUser();

        nullUser.Should().BeNull();
    }
}