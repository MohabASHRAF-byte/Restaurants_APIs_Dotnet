using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Contstants;
using Restaurants.Domain.Entities;
using Restuarants.infrastructure.Persistence;

// Assuming Restaurants is part of the Domain layer

namespace Restuarants.infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantDbContext dbcontext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if ((await dbcontext.Database.GetPendingMigrationsAsync()).Any())
            {
                await dbcontext.Database.MigrateAsync();
            }

            if (!await dbcontext.Database.CanConnectAsync())
                return;
            // delete exsiting data to seed the new one 
            // it can return after the check if we want to stop seeding if data exist

            if (await dbcontext.Restaurants.AnyAsync())
                await dbcontext.Restaurants.ExecuteDeleteAsync();
            var restaurants = GetRestaurants();
            dbcontext.AddRange(restaurants);
            var roles = GetIdentityRoles();
            if (!await dbcontext.Roles.AnyAsync())
            {
                dbcontext.Roles.AddRange(roles);
            }

            await dbcontext.SaveChangesAsync();
        }

        private static IEnumerable<IdentityRole> GetIdentityRoles()
        {
            List<string> roles =
            [
                UserRoles.Admin,
                UserRoles.User,
                UserRoles.Owner
            ];
            return roles.Select(
                role => new IdentityRole { Name = role, NormalizedName = role.ToUpper() }
            ).ToList();
        }



        private IEnumerable<Restaurant> GetRestaurants()
        {
            var user = new User()
            {
                UserName = "admin",
                Email = "Mohab@test.com"
            };
            List<Restaurant> restaurants =
            [
                new Restaurant
                {
                    Name = "McDonald's",
                    Description = "Fast food chain",
                    Category = "Fast Food",
                    HasDelivery = true,
                    ContactEmail = "contact@mcdonalds.com",
                    ContactNumber = "+1234567890",
                    Address = new Address
                    {
                        City = "New York",
                        Street = "5th Avenue",
                        PostalCode = "10-001"
                    },
                    Dishes =
                    [
                        new Dish
                        {
                            Name = "Big Mac",
                            Description = "Double beef burger with special sauce",
                            Price = 5.99M,
                            RestaurantId = 1
                        },

                        new Dish
                        {
                            Name = "McNuggets",
                            Description = "Chicken nuggets with dipping sauces",
                            Price = 4.99M,
                            RestaurantId = 1
                        }
                    ],
                    owner = user
                },
                // KFC without Address

                new Restaurant
                {
                    Name = "KFC",
                    Description = "Famous for fried chicken",
                    Category = "Fast Food",
                    HasDelivery = true,
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "+0987654321",
                    Address = null, // No address
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Original Recipe Chicken",
                            Description = "Fried chicken with 11 herbs and spices",
                            Price = 6.99M,
                            RestaurantId = 2,
                            KiloCalories = 500
                        },
                        new Dish
                        {
                            Name = "Chicken Sandwich",
                            Description = "Crispy chicken sandwich",
                            Price = 5.49M,
                            RestaurantId = 2,
                            KiloCalories = 800
                        }
                    },
                    owner = user
                }
            ];

            return restaurants;
        }
    }
}