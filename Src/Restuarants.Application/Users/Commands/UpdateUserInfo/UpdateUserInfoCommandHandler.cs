using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restuarants.Application.Users.Commands.UpdateUserInfo;

public class UpdateUserInfoCommandHandler(
    ILogger<UpdateUserInfoCommandHandler> logger,
    IUserContext userContext,
    IUserStore<User> userStore
) : IRequestHandler<UpdateUserInfoCommand>
{
    public async Task Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        if(user == null)
            throw new InvalidOperationException("NO USER FOUND");

        logger.LogInformation("updating user {@user} info with {@data}", user!.Id, request);
        var dbUser = await userStore.FindByIdAsync(user!.Id,cancellationToken);
        if(dbUser == null)
            throw new ResourseNotFound(nameof(User), user.Id!);
        dbUser.bithDay =request.bithDay;
        dbUser.Nationality = request.Nationality;
        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}