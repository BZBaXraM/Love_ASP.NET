using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_.Filters;

public class MyAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isLogin = context
            .HttpContext
            .Request
            .Query
            .Any(q => q.Key == "Name" && q.Value == "Admin");
        var isPassword = context
            .HttpContext
            .Request
            .Query
            .Any(q => q.Key == "Password" && q.Value == "Pass123");
        if (!isLogin || !isPassword)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}