using AutoMapper;
using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Entities;
using ClassManager.Business.Enums;
using ClassManager.Business.Errors;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace ClassManager.Business.Tests.Services;

public class CursoServiceTests
{
    private readonly CursoService _sut;
    private readonly INotificationServce _notificationService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IValidator<CriarCursoDto> _criarCursoDtoValidator;
    private readonly IMapper _mapper;
    public CursoServiceTests()
    {
        _notificationService = Substitute.For<INotificationServce>();
        _usuarioRepository = Substitute.For<IUsuarioRepository>();
        _criarCursoDtoValidator = Substitute.For<IValidator<CriarCursoDto>>();
        _cursoRepository = Substitute.For<ICursoRepository>();
        _mapper = Substitute.For<IMapper>();
        _sut = new CursoService(_notificationService, _usuarioRepository, _cursoRepository, _criarCursoDtoValidator, _mapper);
    }

    [Trait("Categoria", "Curso")]
    [Fact(DisplayName = "Criar Curso com dados inválidos deve falhar")]
    public async Task CriarCurso_DadosInvalidos_DeveFalhar()
    {
        // Arrange
        _criarCursoDtoValidator.Validate(Arg.Any<CriarCursoDto>()).Returns(new ValidationResult
        {
            Errors = new List<ValidationFailure>()
            {
                new ValidationFailure()
            }
        });

        //  Act
        var result = await _sut.CriarCurso(new CriarCursoDto());

        // Assert
        Assert.True(result.IsFailed);
        Assert.True(result.HasError<ValidationError>());
    }

    [Trait("Categoria", "Curso")]
    [Fact(DisplayName = "Criar Curso com usuario que não é professor deve falhar")]
    public async Task CriarCurso_UsuarioNaoProfessor_DeveFalhar()
    {
        // Arrange
        var dto = new CriarCursoDto
        {
            ProfessorId = Guid.NewGuid(),
        };
        _criarCursoDtoValidator.Validate(Arg.Any<CriarCursoDto>())
            .Returns(new ValidationResult());
        
        _usuarioRepository.ObterPorId(dto.ProfessorId)
            .Returns(new Usuario()
            {
                Tipo = TipoUsuario.Aluno
            });

        // Act
        var result = await _sut.CriarCurso(dto);

        // Assert
        Assert.True(result.IsFailed);
        Assert.True(result.HasError(p => p.Message == "Professor não foi encontrado"));
        _usuarioRepository.Received(1).ObterPorId(Arg.Any<Guid>());
    }

    [Trait("Categoria", "Curso")]
    [Fact(DisplayName = "Criar Curso deve ser bem sucedido")]
    public async Task CriarCurso_CriaCursoComSucesso()
    {
        // Arrange
        var dto = new CriarCursoDto
        {
            ProfessorId = Guid.NewGuid(),
            Nome = "Nome de curso",
            Tags = new List<string>()
        };

        _criarCursoDtoValidator.Validate(Arg.Any<CriarCursoDto>())
            .Returns(new ValidationResult());

        _usuarioRepository.ObterPorId(dto.ProfessorId)
            .Returns(new Usuario()
            {
                Tipo = TipoUsuario.Professor
            });

        _mapper.Map<CursoDto>(Arg.Any<Curso>())
            .Returns(new CursoDto());

        // Act
        var cursoDto = await _sut.CriarCurso(dto);

        // Assert
        _cursoRepository.Received(1).Adicionar(Arg.Is<Curso>(p => p.ProfessorId == dto.ProfessorId && p.Nome == dto.Nome));
    }
}