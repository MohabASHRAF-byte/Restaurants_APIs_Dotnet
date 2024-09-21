using MediatR;
using Restuarants.Application.Common;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(int id) :IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; } = id;
}