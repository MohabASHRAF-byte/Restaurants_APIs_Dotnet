using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(
    ILogger<GetDishesForRestaurantQueryHandler> logger,
    IMapper mapper,
    IDishRepository dishRepository,
    IRestaurantRepository restaurantRepository
) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
{
    public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request,
        CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new ResourseNotFound("Restaurant", request.RestaurantId.ToString());
        var dishes =await dishRepository.GetAll(request.RestaurantId);
        var dishDtos = mapper.Map<IEnumerable<DishDto>>(dishes);

        return dishDtos;
    }
}