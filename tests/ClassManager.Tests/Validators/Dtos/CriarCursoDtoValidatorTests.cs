using ClassManager.Business.Dtos.Curso;
using FluentValidation.TestHelper;

namespace ClassManager.Business.Tests.Validators.Dtos;

public class CriarCursoDtoValidatorTests 
{
    [Fact(DisplayName = "Validate CriarCursoDto dados invalidos")]
    [Trait("Categoria", "Validators")]
    public void Validate_DadosInvalidos_DeveRetornarComFalhas()
    {
        // Arrange
        var criarCursoDto = new CriarCursoDto
        {
            Nome = null,
            ProfessorId = Guid.Empty
        };

        // Act
        var result = new CriarCursoDtoValidator().TestValidate(criarCursoDto);
    
        // Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Nome);
        result.ShouldHaveValidationErrorFor(p => p.ProfessorId);
    }

    [Fact(DisplayName = "Validate CriarCursoDto dados validos")]
    [Trait("Categoria", "Validators")]
    public void Validate_DadosValidos_DeveRetornarSemFalhas()
    {
        // Arrange
        var criarCursoDto = new CriarCursoDto
        {
            Nome = "Nome",
            ProfessorId = Guid.NewGuid()
        };

        // Act
        var result = new CriarCursoDtoValidator().TestValidate(criarCursoDto);
    
        // Assert
        Assert.True(result.IsValid);
    }
}