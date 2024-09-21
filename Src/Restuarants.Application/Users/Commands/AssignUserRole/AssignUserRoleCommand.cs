using MediatR;

namespace Restuarants.Application.Users.Commands.AssignUserRole;

public class AssignUserRoleCommand:IRequest
{
    public string email { get; set; }
    public string role { get; set; }
}