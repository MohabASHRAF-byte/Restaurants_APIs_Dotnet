namespace Restaurants.Domain.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; }

//
    public string? ContactEmail { get; set; }

    public string? ContactNumber { get; set; }

//
    public Address? Address { get; set; }
    public List<Dish> Dishes { get; set; } = new();

    public string ownerId { get; set; } = default!;
    public User owner { get; set; } = default!;
    public string? LogoUrl { get; set; }
}