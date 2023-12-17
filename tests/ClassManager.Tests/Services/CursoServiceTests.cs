using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Entities;
using ClassManager.Business.Enums;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NSubstitute.ReturnsExtensions;

namespace ClassManager.Business.Tests.Services;

public class CursoServiceTests
{
    private readonly CursoService _sut;
    private readonly INotificationServce _notificationService;
    private readonly IUsuarioRepository _usuarioRepository;
    public CursoServiceTests()
    {
        _notificationService = Substitute.For<INotificationServce>();
        _usuarioRepository = Substitute.For<IUsuarioRepository>();
        _sut = new CursoService(_notificationService, _usuarioRepository);
    }

    [Fact(DisplayName = "Criar Curso sem Professor deve falhar")]
    [Trait("Categoria", "Curso")]
    public async Task CriarCurso_SemProfessor_DeveFalhar()
    {
        // Arrange
        
        //  Act
        await _sut.CriarCurso(new CriarCursoDto());

        // Assert
        _notificationService.Received().Handle(Arg.Any<string>());
    }

    [Trait("Categoria", "Curso")]
    [Fact(DisplayName = "Criar Curso com professor que não existe deve falhar")]
    public async Task CriarCurso_ProfessorNaoExiste_DeveFalhar()
    {
        // Arrange
        var dto = new CriarCursoDto
        {
            ProfessorId = Guid.NewGuid(),
        };
        _usuarioRepository.ObterPorId(dto.ProfessorId)
            .ReturnsNull();
        // Act
         await _sut.CriarCurso(dto);

        // Assert
        _notificationService.Received().Handle(Arg.Any<string>());
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
        _usuarioRepository.ObterPorId(dto.ProfessorId)
            .Returns(new Usuario()
            {
                Tipo = TipoUsuario.Aluno
            });

        // Act
        await _sut.CriarCurso(dto);

        // Assert
        _notificationService.Received().Handle(Arg.Any<string>());
    }
}