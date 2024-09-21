using AutoMapper;
using Restaurants.Domain;
using Restaurants.Domain.Entities;
using Restuarants.Application.Restaurants.Commands.CreateRestaurant;
using Restuarants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restuarants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
  
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(dto => dto.Street, opt => opt.MapFrom(
                src => src.Address == null ? null : src.Address.Street))
            .ForMember(dto => dto.City, opt => opt.MapFrom(
                src => src.Address == null ? null : src.Address.City))
            .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(
                src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(dto => dto.Dishes, opt => opt.MapFrom(
                src => src.Dishes));

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(res => res.Address, opt => opt.MapFrom(
                src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }
            ));
        CreateMap<UpdateRestaurantCommand, Restaurant>();
    }
}