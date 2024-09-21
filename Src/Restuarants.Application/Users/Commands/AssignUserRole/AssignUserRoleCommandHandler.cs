using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restuarants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommandHandler> logger,
    UserManager<User> userManager,
    RoleManager<IdentityRole>roleManager,
    IUserContext userContext
)
    : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.email)
                   ?? throw new ResourseNotFound(nameof(User),request.email);
        var role = await roleManager.FindByNameAsync(request.role)
                        ?? throw new ResourseNotFound(nameof(IdentityRole),request.role);
        
        await userManager.AddToRoleAsync(user, role.Name!);
    }
}