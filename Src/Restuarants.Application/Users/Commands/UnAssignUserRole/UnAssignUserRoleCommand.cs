using MediatR;

namespace Restuarants.Application.Users.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommand: IRequest
{
    public string email { get; set; }
    public string role { get; set; }
}