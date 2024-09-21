using Restaurants.Domain;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Application.Restaurants.Dishes.Dtos;

namespace Restuarants.Application.Restaurants.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;

    public bool HasDelivery { get; set; }

//
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public List<DishDto> Dishes { get; set; } = new();
}