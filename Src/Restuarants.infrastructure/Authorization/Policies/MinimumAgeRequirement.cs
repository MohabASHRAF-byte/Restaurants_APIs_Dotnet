using Microsoft.AspNetCore.Authorization;

namespace Restuarants.infrastructure.Authorization.Policies;

public class MinimumAgeRequirement(int minAge) : IAuthorizationRequirement
{
    public int MinAge { get; } = minAge;
}