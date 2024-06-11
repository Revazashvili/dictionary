using DictionaryApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApi.EndpointFilters;

public class AdminPrivilegesEndpointFilter : IEndpointFilter
{
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AdminPrivilegesEndpointFilter(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated == false)
            return new UnauthorizedResult();

        var user = await _userManager.GetUserAsync(context.HttpContext.User);
        if (user is null)
            return new UnauthorizedResult();
        
        if (!user.IsAdmin && !_webHostEnvironment.IsDevelopment())
            return new UnauthorizedResult();
        
        return await next(context);
    }
}