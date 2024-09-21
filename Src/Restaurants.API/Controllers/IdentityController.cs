using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Contstants;
using Restuarants.Application.Users.Commands.AssignUserRole;
using Restuarants.Application.Users.Commands.UnAssignUserRole;
using Restuarants.Application.Users.Commands.UpdateUserInfo;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/Identity")]
public class IdentityController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("update")]
    [Authorize]
    public async Task<IActionResult> Update(UpdateUserInfoCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("Role")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    [HttpDelete("Role")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> RemoveRole(UnAssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}