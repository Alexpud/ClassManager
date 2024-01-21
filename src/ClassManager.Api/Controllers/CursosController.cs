using System.Net;
using ClassManager.Api.Controllers;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[Route("api/[controller]")]
public class CursosController : BaseController
{
    private readonly CursoService _cursoService;
    public CursosController(
        CursoService cursoService, 
        INotificationServce notificationService) : base(notificationService)
    {
        _cursoService = cursoService;
    }

    /// <summary>
    /// Responsável pela criação do curso
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("")]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(CursoDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Criar(CriarCursoDto dto) 
        => CustomResponse(await _cursoService.CriarCurso(dto));

    [HttpGet]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(List<CursoDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
        => CustomResponse(await _cursoService.ObterTodos());
}