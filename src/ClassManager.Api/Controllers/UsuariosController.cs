using System.Net;
using ClassManager.Business.Authentication;
using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[Route("api/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;
    private readonly IUser user;

    public UsuariosController(
        IUsuarioService usuarioService,
        INotificationServce notificationServce,
        IUser user) : base(notificationServce)
    {
        _usuarioService = usuarioService;
        this.user = user;
    }

    /// <summary>
    /// Responsavel pela criacao de usuario
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Coordenador")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Criar(UsuarioCriacaoDto dto) 
        => CustomResponse(await _usuarioService.Criar(dto));

    /// <summary>
    /// Obtém informações resumidas de um usuário pelo id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}/resumidos")]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(UsuarioDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> ObterDadosResumidosPorId(Guid id)
    {
        user.GetUserId();
        var usuario = await _usuarioService.ObterDadosResumidosPorId(id);
        if (usuario == null)
            return NoContent();

        return Ok(usuario);
    }

    /// <summary>
    /// Obtém todos os registros de usuarios com informacoes resumidas
    /// </summary>
    /// <returns></returns>
    [HttpGet("resumidos")]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
       => Ok(await _usuarioService.ObterTodos());
}