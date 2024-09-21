using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restuarants.infrastructure.Persistence;

namespace Restuarants.infrastructure.Repositories;

public class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository

{
    public async Task<(int, IEnumerable<Restaurant>)> GetAllAsync(string? searchName, int pageNumber, int pageSize)
    {
        searchName ??= string.Empty;
        searchName = searchName.ToLower();
        var baseQuery = dbContext.Restaurants
            .Where(r => r.Name.ToLower().Contains(searchName));
        var totalCount = await baseQuery.CountAsync();
        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
        return (totalCount, restaurants);
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }

    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        dbContext.Restaurants.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> CountRestaurantsAsync(string id)
    {
        return await dbContext.Restaurants
            .CountAsync(r => r.ownerId == id);
    }
}