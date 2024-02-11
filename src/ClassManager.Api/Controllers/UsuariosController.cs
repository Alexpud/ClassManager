using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;
using ClassManager.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClassManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Responsavel pela criacao de usuario
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    // [Authorize(Roles = "Coordenador")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Criar(UsuarioCriacaoDto dto)
    {
        var result = await _usuarioService.Criar(dto);
        if (result.IsFailed)
            return BadRequest(result);
        return Ok(result.Value);
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
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(IEnumerable<UsuarioDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
       => Ok(await _usuarioService.ObterTodos());
}