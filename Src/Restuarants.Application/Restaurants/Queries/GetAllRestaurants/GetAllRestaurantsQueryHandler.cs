using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Common;
using Restuarants.Application.Restaurants.Commands.CreateRestaurant;
using Restuarants.Application.Restaurants.Dtos;

namespace Restuarants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(
    ILogger<CreateRestaurantCommand> logger,
    IMapper mapper,
    IRestaurantRepository restaurantRepository
) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
{
    public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all restaurants.");
        var restaurants =
            await restaurantRepository.GetAllAsync(request.SearchName, request.PageNumber, request.PageSize);
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants.Item2);
        var count = restaurants.Item1;
        var ret = new PageResult<RestaurantDto>(restaurantsDtos, count, request.PageSize, request.PageNumber);
        return ret;
    }
}