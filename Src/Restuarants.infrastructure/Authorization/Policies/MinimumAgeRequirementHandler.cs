using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Users;

namespace Restuarants.infrastructure.Authorization.Policies;

public class MinimumAgeRequirementHandler(
    ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext
) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        MinimumAgeRequirement requirement)
    {
        var user = userContext.GetCurrentUser()!;
        if (user.BirthDate == null)
        {
            logger.LogInformation("User has no birth date");
            context.Fail();
            return Task.CompletedTask;
        }

        if (user.BirthDate.Value.AddYears(requirement.MinAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("{@User} has a valid birth date", user.Email);
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("{@User} birthdate is invalid", user.Email);
            context.Fail();
        }

        return Task.CompletedTask;
    }
}