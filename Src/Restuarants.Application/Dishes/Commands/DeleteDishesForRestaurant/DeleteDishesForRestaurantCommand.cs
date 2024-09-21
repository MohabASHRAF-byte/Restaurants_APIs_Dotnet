using MediatR;

namespace Restuarants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommand(int id) : IRequest
{
    public int RestaurantId { get; set; } = id;

}