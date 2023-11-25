using System.Security.Claims;
using ClassManager.Business.Authentication;

namespace ClassManager.Api.Identity;

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAcessor;

    public AspNetUser(IHttpContextAccessor httpContextAcessor)
    {
        _httpContextAcessor = httpContextAcessor;
    }

    public Guid GetUserId()
    {
        if (!IsAuthenticated())
            return Guid.Empty;

        var claim = _httpContextAcessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        return Guid.Parse(claim.Value);
    }

    private bool IsAuthenticated() 
        => _httpContextAcessor.HttpContext.User.Identity.IsAuthenticated;
}