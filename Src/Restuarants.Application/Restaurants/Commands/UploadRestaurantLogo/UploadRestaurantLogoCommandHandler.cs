using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Contstants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restuarants.Application.Restaurants.Commands.UploadRestaurantLogo;

public class UploadRestaurantLogoCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantRepository restaurantRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IBlobStorageService blobStorageService
) : IRequestHandler<UploadRestaurantLogoCommand, string>
{
    public async Task<string> Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.Restaurant);
        if (restaurant == null)
            throw new ResourseNotFound(nameof(restaurant), request.Restaurant.ToString());
        // if (!restaurantAuthorizationService.IsAuthorized(restaurant, ResourceOperationType.Create))
        //     throw new UnauthorizedAccessException();
        var logoUri = await blobStorageService.UploadAsync(request.File, restaurant.Name);
        restaurant.LogoUrl = logoUri;
        await restaurantRepository.SaveChangesAsync();
        return logoUri;
    }
}