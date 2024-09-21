using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Repositories;
using Restuarants.infrastructure.Persistence;

namespace Restuarants.infrastructure.Repositories;

public class DishRepository(
    RestaurantDbContext dbContext
) : IDishRepository
{
    public async Task CreateAsync(Dish dish)
    {
        dbContext.Add(dish);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetAll(int requestRestaurantId)
    {
        var dishes = await dbContext.Dishes
            .Where(d => d.RestaurantId == requestRestaurantId)
            .ToListAsync();
        return dishes;
    }

    public async Task<Dish?> GetDishById(int requestRestaurantId, int dishId)
    {
        var dish = await dbContext.Dishes
            .FirstOrDefaultAsync(d => d.RestaurantId == requestRestaurantId && d.Id == dishId);
        return dish;
    }

    public async Task DeleteForId(IEnumerable<Dish> dishes)
    {
        dbContext.Dishes.RemoveRange(dishes);
        await dbContext.SaveChangesAsync();
    }
}