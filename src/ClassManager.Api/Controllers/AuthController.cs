using ClassManager.Business.Dtos.Authentication;
using ClassManager.Business.Notifications;
using ClassManager.Data.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClassManager.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IIdentityService _identityService;
    public AuthController(INotificationServce notificationService, IIdentityService identityService) : base(notificationService)
    {
        _identityService = identityService;
    }

    /// <summary>
    /// Realiza o login com as informações de acesso e retorna o token JWT
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("api/Login")]
    [ProducesResponseType(typeof(LoginResponseDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login(LoginDto dto)
        => CustomResponse(await _identityService.Login(dto));

    /// <summary>
    /// Cria uma nova role no Identity
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("api/roles")]
    [Authorize(Roles = "Coordenador")]
    public async Task<IActionResult> CreateRole(RoleCreateDto dto)
    {
        await _identityService.CriarRole(dto.Nome.ToString());
        return CustomResponse();
    }
}