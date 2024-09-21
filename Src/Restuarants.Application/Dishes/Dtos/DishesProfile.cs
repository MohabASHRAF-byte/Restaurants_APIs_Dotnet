using AutoMapper;
using Restaurants.Domain;
using Restuarants.Application.Dishes.Commands.CreateDish;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Restaurants.Dishes.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishCommand, Dish>();
    }
}