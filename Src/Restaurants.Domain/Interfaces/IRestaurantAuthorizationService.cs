using Restaurants.Domain.Contstants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    public bool IsAuthorized(Restaurant restaurant, ResourceOperationType operationType);
}