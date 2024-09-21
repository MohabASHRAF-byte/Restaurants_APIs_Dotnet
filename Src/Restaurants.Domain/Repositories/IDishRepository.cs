namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    public Task CreateAsync(Dish dish);
    public Task<IEnumerable<Dish>> GetAll(int requestRestaurantId);
    Task<Dish?> GetDishById(int requestRestaurantId, int dishId);
    Task DeleteForId(IEnumerable<Dish> dishes);
}