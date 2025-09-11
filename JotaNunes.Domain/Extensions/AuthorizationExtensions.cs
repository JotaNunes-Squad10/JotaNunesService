using JotaNunes.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace JotaNunes.Domain.Extensions;

public static class AuthorizationExtensions
{
    public static void AddGroupPolicies(this AuthorizationOptions options)
    {
        foreach (Group group in Enum.GetValues(typeof(Group)))
        {
            string groupName = group.GetName();
            options.AddPolicy(group.ToString(), policy =>
                policy.RequireClaim("groups", groupName));
        }
    }
}