using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JotaNunes.Domain.Models.Keycloak;

public class AuthUser : User
{
    public AuthUser(IHttpContextAccessor accessor)
    {
        var user = accessor.HttpContext?.User;

        var sub = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = user?.FindFirst("preferred_username")?.Value;
        var email = user?.FindFirst("email")?.Value;
        var emailVerified = user?.FindFirst("email_verified")?.Value;
        var firstName = user?.FindFirst("given_name")?.Value;
        var lastName = user?.FindFirst("family_name")?.Value;

        Id = Guid.TryParse(sub, out var id) ? id : Guid.Empty;
        Username = username ?? string.Empty;
        Email = email;
        EmailVerified = bool.TryParse(emailVerified, out var emailV) && emailV;
        Enabled = true;
        FirstName = firstName;
        LastName = lastName;
    }
}