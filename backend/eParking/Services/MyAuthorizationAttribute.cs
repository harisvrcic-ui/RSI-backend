namespace eParking.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class MyAuthorizationAttribute(bool isAdmin, bool isUser, bool allowAnonymous = false) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (allowAnonymous)
            return;

        var authService = context.HttpContext.RequestServices.GetService<IMyAuthService>();
        if (authService == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authInfo = authService.GetAuthInfoFromRequest();
        if (authInfo == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        bool allowed = (!isAdmin && !isUser)
            ? authInfo.IsLoggedIn
            : (isAdmin && authInfo.IsAdmin) || (isUser && authInfo.IsUser);
        if (!allowed)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
