using AutoMapper;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClassManager.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly CursoService _cursoService;
    private readonly ICursoRepository _cursoRepository;
    private readonly IMapper _mapper;

    public CursosController(CursoService cursoService,
        ICursoRepository cursoRepository,
        IMapper mapper)
    {
        _cursoService = cursoService;
        _cursoRepository = cursoRepository;
        _mapper = mapper;
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

    /// <summary>
    /// Responsável por listar todos os cursos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(List<CursoDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterTodos()
        => Ok(_mapper.Map<List<CursoDto>>(await _cursoRepository.ObterTodos()));

    /// <summary>
    /// Responsável por obter curso pelo ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "Discentes")]
    [ProducesResponseType(typeof(CursoDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ObterPorId(Guid id)
        => Ok(_mapper.Map<CursoDto>(await _cursoRepository.ObterPorId(id)));

}