using System.Net;
using System.Reflection.Metadata.Ecma335;
using ClassManager.Api.Controllers;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly CursoService _cursoService;
    public CursosController(CursoService cursoService)
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
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Criar(CriarCursoDto dto)
    { 
        var result = await _cursoService.CriarCurso(dto);
        if (result.IsFailed)
            return BadRequest(result);

        return Ok(result.Value);
    }

    [HttpGet]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(List<CursoDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
        => Ok(await _cursoService.ObterTodos());
}