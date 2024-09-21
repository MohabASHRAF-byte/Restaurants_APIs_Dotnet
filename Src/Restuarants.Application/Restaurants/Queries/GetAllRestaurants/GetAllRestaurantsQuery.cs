using MediatR;
using Restaurants.Domain.Entities;
using Restuarants.Application.Common;
using Restuarants.Application.Restaurants.Dtos;

namespace Restuarants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PageResult<RestaurantDto>>
{
    public string? SearchName { get; set; } = "";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}