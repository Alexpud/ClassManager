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
        };

        // Act
        var result = new CriarCursoDtoValidator().TestValidate(criarCursoDto);
    
        // Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(p => p.Nome).WithErrorMessage("Não pode criar curso sem nome");
    }

    [Fact(DisplayName = "TestValidate CriarCursoDto dados validos deve ser bem sucedido")]
    [Trait("Categoria", "Validators")]
    public void TestValidate_ExecutaComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var criarCursoDto = new CriarCursoDto
        {
            Nome = "Nome",
        };

        // Act
        var result = new CriarCursoDtoValidator().TestValidate(criarCursoDto);
    
        // Assert
        Assert.True(result.IsValid);
    }
}