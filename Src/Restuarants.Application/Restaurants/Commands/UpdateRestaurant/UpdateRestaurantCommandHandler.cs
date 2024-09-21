using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Restaurants.Commands.DeleteRestaurant;

namespace Restuarants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository
    ):IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
            throw new ResourseNotFound(nameof(restaurant),request.Id.ToString());
        mapper.Map(request, restaurant);
        await restaurantRepository.SaveChangesAsync();
    }
}