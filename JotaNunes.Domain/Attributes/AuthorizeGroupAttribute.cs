using JotaNunes.Domain.Models.Keycloak;
using Microsoft.AspNetCore.Authorization;

namespace JotaNunes.Domain.Attributes;

public class AuthorizeGroupAttribute : AuthorizeAttribute
{
    public AuthorizeGroupAttribute(Group group)
    {
        Policy = group.ToString();
    }
}