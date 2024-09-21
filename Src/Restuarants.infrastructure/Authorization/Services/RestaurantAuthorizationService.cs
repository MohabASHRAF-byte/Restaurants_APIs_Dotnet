using Microsoft.Extensions.Logging;
using Restaurants.Domain.Contstants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restuarants.Application.Users;

namespace Restuarants.infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(
    ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext
) : IRestaurantAuthorizationService
{
    public bool IsAuthorized(Restaurant restaurant, ResourceOperationType operationType)
    {
        var currentUser = userContext.GetCurrentUser()!;
        logger.LogInformation("Authenticating {@user} , to {@op} for {@res}",
            currentUser.Email,
            operationType,
            restaurant.Name
        );
        if (operationType == ResourceOperationType.Read || operationType == ResourceOperationType.Create)
        {
            logger.LogInformation("Create / Read operation - successful authorization");
            return true;
        }

        if ((operationType == ResourceOperationType.Update || operationType == ResourceOperationType.Delete)
            && (currentUser.IsInRole(UserRoles.Admin) || currentUser.Id == restaurant.ownerId)
           )
        {
            logger.LogInformation("Update / Delete operation - successful authorization");
            return true;
        }

        return false;
    }
}