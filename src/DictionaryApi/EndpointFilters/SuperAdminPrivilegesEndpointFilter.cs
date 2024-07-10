using DictionaryApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.EndpointFilters;

public class SuperAdminPrivilegesEndpointFilter : IEndpointFilter
{
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SuperAdminPrivilegesEndpointFilter(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated == false)
            return new UnauthorizedResult();

        var user = await _userManager.GetUserAsync(context.HttpContext.User);
        if (user is null)
            return new UnauthorizedResult();
        
        if (!user.IsSuperAdmin && !_webHostEnvironment.IsDevelopment())
            return new UnauthorizedResult();
        
        return await next(context);
    }
}