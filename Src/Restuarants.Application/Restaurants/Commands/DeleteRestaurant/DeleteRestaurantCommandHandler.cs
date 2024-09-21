using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restuarants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(
    ILogger<DeleteRestaurantCommand> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository
) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete restaurant {request.Id}");
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
            throw new ResourseNotFound(nameof(restaurant),request.Id.ToString());
        await restaurantRepository.Delete(restaurant);
    }
}