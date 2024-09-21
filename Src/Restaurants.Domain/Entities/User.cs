using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly? bithDay { get; set; }
    public string? Nationality { get; set; }

    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}