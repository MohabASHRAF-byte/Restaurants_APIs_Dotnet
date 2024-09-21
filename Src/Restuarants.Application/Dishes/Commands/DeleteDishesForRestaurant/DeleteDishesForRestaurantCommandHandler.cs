using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restuarants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommandHandler(
    ILogger<GetDishesForRestaurantQueryHandler> logger,
    IMapper mapper,
    IDishRepository dishRepository,
    IRestaurantRepository restaurantRepository
    ):IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new ResourseNotFound("Restaurant", request.RestaurantId.ToString());
        var dishes =await dishRepository.GetAll(request.RestaurantId);
        await dishRepository.DeleteForId(dishes);
    }
}