using MediatR;

namespace Restuarants.Application.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommand:IRequest
{
    public DateOnly bithDay { get; set; }
    public string Nationality { get; set; }
}