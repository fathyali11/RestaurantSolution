using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurats.Domain.Constants;
using Restaurats.Domain.Entities;
using Restaurats.Infrastructure.Presistence;

namespace Restaurats.Infrastructure.Seeders;
internal class RestaurantSeeder(RestaurantDbContext context) : IRestaurantSeeder
{
    private readonly RestaurantDbContext _context = context;
    public async Task SeedAsync()
    {
        if(_context.Database.GetPendingMigrations().Any())
        {
            await _context.Database.MigrateAsync();
        }
        if (await _context.Database.CanConnectAsync())
        {
            if (!await _context.Restaurants.AnyAsync())
            {
                await _context.Restaurants.AddRangeAsync(GetRestaurants());
                await _context.SaveChangesAsync();
            }
            if(!await _context.Roles.AnyAsync())
            {
                await _context.Roles.AddRangeAsync(GetRoles());
                await _context.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new() { Name = UserRoles.AdminRole, NormalizedName = UserRoles.AdminRole.ToUpper(),ConcurrencyStamp=Guid.CreateVersion7().ToString() },
            new() { Name = UserRoles.OwnerRole, NormalizedName = UserRoles.OwnerRole.ToUpper(),ConcurrencyStamp=Guid.CreateVersion7().ToString() },
            new() { Name = UserRoles.UserRole, NormalizedName = UserRoles.UserRole.ToUpper() ,ConcurrencyStamp=Guid.CreateVersion7().ToString()}
        ];
        return roles;
    }
    private IEnumerable<Restaurant> GetRestaurants()
    {
        ApplicationUser owner = new()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "owner",
            NormalizedUserName = "OWNER",
            Email = "owner@gmail.com",
            NormalizedEmail ="OWNER@GMAIL.COM",
            EmailConfirmed = true,
        };
        List <Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                },
                OwnerId= owner.Id,
                Owner=owner
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    PostalCode = "W1F 8SR"
                },
                OwnerId= owner.Id,
                Owner=owner

            }
        ];

        return restaurants;
    }
}