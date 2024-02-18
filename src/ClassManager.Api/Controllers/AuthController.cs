using ClassManager.Business.Dtos.Authentication;
using ClassManager.Data.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClassManager.Api.Controllers;

[Authorize]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IdentityService _identityService;
    public AuthController(IdentityService identityService)
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
    {
        var result = await _identityService.Login(dto);
        if (result.IsFailed)
            return BadRequest(result);
        return Ok(result.Value);
    }

    /// <summary>
    /// Cria uma nova role no Identity
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    [HttpPost("api/roles")]
    [Authorize(Roles = "Coordenador")]
    public async Task<IActionResult> CreateRole(RoleCreateDto dto)
    {
        var result  =await _identityService.CriarRole(dto.Nome.ToString());
        if (result.IsFailed)
            return BadRequest(result);
        return NoContent();
    }
}