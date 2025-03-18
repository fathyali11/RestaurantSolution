using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurats.Domain.Entities;

namespace Restaurats.Infrastructure.Presistence;
internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options):IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Restaurant> Restaurants { get; set; } = default!;
    public DbSet<Dish> Dishes { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne(x=>x.Restaurant)
            .HasForeignKey(d => d.RestaurantId);

        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.OwnedRestaurants)
            .WithOne(r => r.Owner)
            .HasForeignKey(r => r.OwnerId);
    }

}
