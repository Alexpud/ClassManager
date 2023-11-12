using System.Net;
using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[Route("api/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService, INotificationServce notificationServce) : base(notificationServce) 
    {
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
}
