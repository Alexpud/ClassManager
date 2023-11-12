using System.Net;
using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using ClassManager.Business.Repositories;
using ClassManager.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[Route("api/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosController(
        IUsuarioService usuarioService, 
        IUsuarioRepository usuarioRepository,
        INotificationServce notificationServce) : base(notificationServce) 
    {
        _usuarioRepository = usuarioRepository;
        _usuarioService = usuarioService;
    }

    /// <summary>
    /// Responsavel pela criacao de usuario
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(CustomProblemDetails), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create(UsuarioCriacaoDto dto)
    {
        var usuarioCriado = await _usuarioService.Adicionar(dto);
        return CustomResponse(usuarioCriado);
    }

    /// <summary>
    /// Obtém um usuário pelo id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        if (usuario == null)
            return NoContent();

        return Ok(usuario);
    }

    /// <summary>
    /// Obtém todos os registros de usuarios
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Usuario>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
        => Ok(await _usuarioRepository.ObterTodos());

    /// <summary>
    /// Rmove um usuario pelo id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        await _usuarioService.Remover(id);
        return Ok();
    }
}
