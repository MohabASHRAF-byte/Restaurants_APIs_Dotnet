using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restuarants.Application.Users;

public interface IUserContext
{
    public CurrentUser? GetCurrentUser();
}

internal class UserContext(
    IHttpContextAccessor httpContextAccessor
) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user == null)
            throw new InvalidOperationException("User Context is not present");
        if (user.Identity == null || !user.Identity.IsAuthenticated)
            return null;
        var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        var roles = user.FindAll(c => c.Type == ClaimTypes.Role)?.Select(c => c.Value).ToList();
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var birthDateStr = user.FindFirst(c => c.Type == "BirthDay")?.Value;
        var birthDate = (birthDateStr == null)
            ? (DateOnly?)null
            : DateOnly.Parse(birthDateStr);
        var currentUser = new CurrentUser(id, email, roles, nationality, birthDate);
        return currentUser;
    }
}