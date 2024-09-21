using FluentValidation;

namespace Restuarants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator:AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(r => r.Name)
            .Length(2, 50)
            .WithMessage("Name must be between 2 and 50 characters");
    }
}