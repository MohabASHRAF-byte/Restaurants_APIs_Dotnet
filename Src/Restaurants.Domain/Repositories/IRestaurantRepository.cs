using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantRepository
{
    public Task<(int, IEnumerable<Restaurant>)> GetAllAsync(string? searchName, int pageNumber, int pageSize);
    public Task<Restaurant?> GetByIdAsync(int id);
    public Task<int> Create(Restaurant restaurant);
    public Task Delete(Restaurant requestId);
    public Task SaveChangesAsync();
    public Task<int> CountRestaurantsAsync(string id);
}