using MediatR;

namespace Restuarants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}