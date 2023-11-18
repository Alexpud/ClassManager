using System.Net;
using ClassManager.Business.Dtos;
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

    public UsuariosController(
        IUsuarioService usuarioService,
        INotificationServce notificationServce) : base(notificationServce)
    {
        _usuarioService = usuarioService;
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
    {
        var usuarioCriado = await _usuarioService.Criar(dto);
        return CustomResponse(usuarioCriado);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
    {
        return CustomResponse(await _usuarioService.Login(dto));
    }

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
    [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
       => Ok(await _usuarioService.ObterTodos());

    ///// <summary>
    ///// Rmove um usuario pelo id
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Deletar(Guid id)
    //{
    //    await _usuarioService.Remover(id);
    //    return Ok();
    //}
}