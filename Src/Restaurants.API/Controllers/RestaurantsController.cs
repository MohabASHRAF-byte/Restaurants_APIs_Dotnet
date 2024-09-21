using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Contstants;
using Restuarants.Application.Restaurants;
using Restuarants.Application.Restaurants.Commands.CreateRestaurant;
using Restuarants.Application.Restaurants.Commands.DeleteRestaurant;
using Restuarants.Application.Restaurants.Commands.UpdateRestaurant;
using Restuarants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restuarants.Application.Restaurants.Dtos;
using Restuarants.Application.Restaurants.Queries.GetAllRestaurants;
using Restuarants.Application.Restaurants.Queries.GetRestaurantById;
using Restuarants.infrastructure.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/Restaurants")]
public class RestaurantsController(
    IMediator mediator
) : ControllerBase
{
    [HttpGet]
    // [Authorize]
    // [Author ize(policy: PolicyNames.HasNationality)]
    // [Authorize(policy: PolicyNames.AtLeast20)]
    // [Authorize(policy: PolicyNames.AtLeast2Restaurant)]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] GetAllRestaurantsQuery query)
    {
        var resaurants = await mediator.Send(query);
        return Ok(resaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        return Ok(restaurant);
    }

    [HttpPost(nameof(CreateRestaurant))]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(
            Summary = "Deletes a Restaurant",
            Description = "This action deletes the Restaurant with the passed ID" +
                          "and returns a 204 No Content response if successful."
        )
    ]
    [SwaggerResponse(204, "No content returned, resource deleted")]
    [SwaggerResponse(404, "Resource not found")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteRestaurantCommand(id));
        return NoContent();
    }

    [HttpPatch("Update")]
    [Authorize(Roles = UserRoles.Admin)]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> Update(UpdateRestaurantCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var command = new UploadRestaurantLogoCommand()
        {
            Restaurant = id,
            FileName = file.FileName,
            File = stream
        };
        await mediator.Send(command);
        return NoContent();
    }
}