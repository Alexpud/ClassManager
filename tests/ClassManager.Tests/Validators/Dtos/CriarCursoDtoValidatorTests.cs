using ClassManager.Business.Dtos.Curso;
using FluentValidation.TestHelper;

namespace ClassManager.Business.Tests.Validators.Dtos;

public class CriarCursoDtoValidatorTests 
{
    [Fact(DisplayName = "TestValidate CriarCursoDto dados invalidos deve falhar")]
    [Trait("Categoria", "Validators")]
    public void TestValidate_RetornarComFalhas_QuandoDadosInvalidos()
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
        result.ShouldHaveValidationErrorFor(p => p.Nome).WithErrorMessage("Não pode criar curso sem nome");
        result.ShouldHaveValidationErrorFor(p => p.ProfessorId).WithErrorMessage("Não pode criar curso sem professor");
    }

    [Fact(DisplayName = "TestValidate CriarCursoDto dados validos deve ser bem sucedido")]
    [Trait("Categoria", "Validators")]
    public void TestValidate_ExecutaComSucesso_QuandoDadosValidos()
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