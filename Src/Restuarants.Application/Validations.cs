using FluentValidation;
using Restuarants.Application.Restaurants.Dtos;

namespace Restuarants.Application;

public class Validations
{
    // the context should be changed depend on the validator
      /*public static void CheckDescription(string description, ValidationContext<CreatRestaurantDto> context)
    {
        if(description.Length>3)
            context.AddFailure("Description must be less than 3 characters");
    }*/
}