using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Entities;

namespace Restuarants.infrastructure.Persistence;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
    : IdentityDbContext<User>(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    // Fluent API configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.Address);

        // Configuring the relationship between Restaurant and Dish
        modelBuilder.Entity<Restaurant>()
            .HasMany(restaurant => restaurant.Dishes)
            .WithOne()
            .HasForeignKey(dish => dish.RestaurantId);
    }
}