using ClassManager.Business.Dtos;
using ClassManager.Business.Notifications;
using ClassManager.Data.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IIdentityService _identityService;
    public AuthController(INotificationServce notificationService, IIdentityService identityService) : base(notificationService)
    {
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
        => CustomResponse(await _identityService.Login(dto));
}