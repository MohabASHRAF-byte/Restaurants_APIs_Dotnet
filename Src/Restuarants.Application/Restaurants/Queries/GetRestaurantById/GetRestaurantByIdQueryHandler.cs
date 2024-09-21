using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Restaurants.Commands.CreateRestaurant;
using Restuarants.Application.Restaurants.Dtos;

namespace Restuarants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(
    ILogger<CreateRestaurantCommand>logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository
    
    ):IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Retrieving restaurant with id : {request.id}.");
        var restaurant = await restaurantRepository.GetByIdAsync(request.id);
        if(restaurant == null)
            throw new ResourseNotFound(nameof(restaurant),request.id.ToString());
        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
        logger.LogInformation("Returning restaurant : {@restaurant}.", restaurantDto);
        return restaurantDto;
    }
}