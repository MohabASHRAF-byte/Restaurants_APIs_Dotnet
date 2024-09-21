using FluentValidation;

namespace Restuarants.Application.Restaurants.Commands.CreateRestaurant;

public class CreatRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
{
    private static void CheckDescription(string description, ValidationContext<CreateRestaurantCommand> context)
    {
        if (description.Length > 100)
            context.AddFailure("Description must be less than 100characters");
    }

    public CreatRestaurantDtoValidator()
    {
        List<string> categories =
        [
            "Indian",
            "Egyptian",
            "French fries",
            "Hawaiian",
            "Italian",
        ];
        RuleFor(restaurant => restaurant.Name)
            .Length(2, 30)
            .WithMessage("length of name must be between 2 and 30 characters");
        RuleFor(restaurant => restaurant.Description)
            .Custom(CheckDescription);
        RuleFor(restaurant => restaurant.PostalCode)
            .Matches(@"^\d{2}-\d{3}$");
        RuleFor(restaurant => restaurant.Category)
            .Must(n => categories.Contains(n)).WithMessage("invalid category");
    }
}