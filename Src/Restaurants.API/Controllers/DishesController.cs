using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Dishes.Commands.CreateDish;
using Restuarants.Application.Dishes.Commands.DeleteDishesForRestaurant;
using Restuarants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restuarants.Application.Dishes.Queries.GetDishesForRestaurant;
using Restuarants.Application.Restaurants.Queries.GetAllRestaurants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/Restaurants/{RestaurantId}/[controller]")]
public class DishesController(
    IMediator mediator
) : ControllerBase
{
    
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetDish([FromRoute] int RestaurantId, [FromRoute] int Id)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(Id, RestaurantId));
        return Ok(dish);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDishes([FromRoute] int RestaurantId)
    {
        var id = (RestaurantId);
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(id));
        return Ok(dishes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int RestaurantId, [FromBody] CreateDishCommand command)
    {
        command.RestaurantId = RestaurantId;
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDish), new { Id = result, RestaurantId = RestaurantId }, null);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteDish([FromRoute] int RestaurantId)
    {
        var command = new DeleteDishesForRestaurantCommand(RestaurantId);
        await mediator.Send(command);
        return NoContent();
    }
}