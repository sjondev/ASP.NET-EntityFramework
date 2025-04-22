﻿using System.Security.Claims;
using BlogApi.Models;

namespace BlogApi.Extensions;

public static class RoleClaimsExtentions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };
        
        result.AddRange(
            user
                    .Roles
                    .Select(role => 
                        new Claim(ClaimTypes.Role, role.Slug)
                    ));

        return result;
    }
}