using System.Security.Claims;

namespace ToDo_Web_APi.Providers;

public class RequestUserProvider : IRequestUserProvider
{
    private readonly HttpContext? _content;

    public RequestUserProvider(IHttpContextAccessor content)
    {
        _content = content.HttpContext;
    }

    public UserInfo? GetUserInfo()
    {
        if (!_content!.User.Claims.Any()) return null;

        var userId = _content.User.Claims.First(c => c.Type == "userId").Value;
        var username = _content.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

        return new UserInfo(userId, username);
    }
}