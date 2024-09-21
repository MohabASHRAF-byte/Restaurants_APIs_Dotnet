using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Users;

namespace Restuarants.infrastructure.Authorization.Policies.Have2Restaurants;

public class Have2RestaurantsHandler(
    IUserContext userContext,
    IRestaurantRepository restaurantRepository
) : AuthorizationHandler<Have2Restaurants>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        Have2Restaurants requirement)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
            throw new Exception("You are not logged in");
        var count = await restaurantRepository.CountRestaurantsAsync(currentUser.Id);
        if (count >= 2)
            context.Succeed(requirement);
        else
        {
            context.Fail();
        }
    }
}