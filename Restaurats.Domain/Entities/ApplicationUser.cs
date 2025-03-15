using Microsoft.AspNetCore.Identity;

namespace Restaurats.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }

    public List<Restaurant> OwnedRestaurants { get; set; } = [];
}