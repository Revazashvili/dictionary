using DictionaryApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.EndpointFilters;

public class AdminPrivilegesEndpointFilter : IEndpointFilter
{
    private static readonly List<string> Roles =
    [
        "admin",
        "super_admin"
    ];
    
    private readonly UserManager<User> _userManager;

    public AdminPrivilegesEndpointFilter(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if(context.HttpContext.User.Identity?.IsAuthenticated == false)
            return new UnauthorizedResult();

        var user = await _userManager.GetUserAsync(context.HttpContext.User);
        if(user is null)
            return new UnauthorizedResult();
        
        if(!Roles.Contains(user.Role))
            return new UnauthorizedResult();
        
        return await next(context);
    }
}