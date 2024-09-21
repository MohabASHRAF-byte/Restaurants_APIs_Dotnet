using MediatR;
using Restaurants.Domain;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery(int RId, int DId) : IRequest<DishDto>
{
    public int RestaurantId { get; set; } = RId;
    public int DishId { get; set; } = DId;
}