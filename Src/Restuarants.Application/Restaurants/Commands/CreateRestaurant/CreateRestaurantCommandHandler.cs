using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Contstants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Users;
using Restuarants.Application.Users.Commands.AssignUserRole;

namespace Restuarants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(
    ILogger<CreateRestaurantCommand> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IUserContext userContext,
    IMediator mediator
) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        logger.LogInformation($"Creating restaurant ...");
        var restaurant = mapper.Map<Restaurant>(request);
        if (!restaurantAuthorizationService.IsAuthorized(restaurant, ResourceOperationType.Create))
        {
            throw new UnauthorizedAccessException();
        }

        restaurant.ownerId = user.Id;
        var id = await restaurantRepository.Create(restaurant);
        var addRole = new AssignUserRoleCommand
        {
            email = user.Email,
            role = UserRoles.Owner
        };
        await mediator.Send(addRole);
        return id;
    }
}