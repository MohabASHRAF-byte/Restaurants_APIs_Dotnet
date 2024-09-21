using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restuarants.Application.Users.Commands.AssignUserRole;

namespace Restuarants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole>roleManager
    ): IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.email)
                   ?? throw new ResourseNotFound(nameof(User),request.email);
        var role = await roleManager.FindByNameAsync(request.role)
                   ?? throw new ResourseNotFound(nameof(IdentityRole),request.role);
        
        await userManager.RemoveFromRoleAsync(user, role.Name!);
    }
}