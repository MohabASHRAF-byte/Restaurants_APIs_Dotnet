using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restuarants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(
    ILogger<GetDishesForRestaurantQueryHandler> logger,
    IMapper mapper,
    IDishRepository dishRepository,
    IRestaurantRepository restaurantRepository
    ):IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant is null)
            throw new ResourseNotFound("Restaurant", request.RestaurantId.ToString());
        var dish =await dishRepository.GetDishById(request.RestaurantId,request.DishId);
        if(dish is null)
            throw new ResourseNotFound("Dish", request.RestaurantId.ToString());
        var dishDto = mapper.Map<DishDto>(dish);
        return dishDto;
    }
}