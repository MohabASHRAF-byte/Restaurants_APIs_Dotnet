using MediatR;
using Restuarants.Application.Restaurants.Dtos;

namespace Restuarants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQuery:IRequest<RestaurantDto>
{
    public int  id { get; set; }

    public GetRestaurantByIdQuery(int _id)
    {
        id = _id;
    }
}