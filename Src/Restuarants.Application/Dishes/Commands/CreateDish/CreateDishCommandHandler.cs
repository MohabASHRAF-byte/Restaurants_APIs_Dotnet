using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Contstants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(
    ILogger<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository,
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService
) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new ResourseNotFound("Restaurant", request.RestaurantId.ToString());
        if (!restaurantAuthorizationService.IsAuthorized(restaurant, ResourceOperationType.Update))
        {
            throw new ForBidenException("Unauthorized Access");
        }

        var dish = mapper.Map<Dish>(request);

        await dishRepository.CreateAsync(dish);
        return dish.Id;
    }
}