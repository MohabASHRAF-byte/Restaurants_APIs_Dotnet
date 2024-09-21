using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;

namespace Restuarants.infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options)
    : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var principal = await base.GenerateClaimsAsync(user);
        if (user.Nationality != null)
        {
            principal.AddClaim(new Claim(ClaimsTypes.Nationality, user.Nationality));
        }

        if (user.bithDay != null)
        {
            principal.AddClaim(new Claim(ClaimsTypes.BirthDay, user.bithDay.ToString()!));
        }

        return new ClaimsPrincipal(principal);
    }
}