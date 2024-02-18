using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;
using ClassManager.Business.Validators.Entities;
using FluentValidation.TestHelper;

namespace ClassManager.Business.Tests.Validators.Dtos;

public class CriarUsuarioDtoValidatorTests
{
    private readonly CriarUsuarioDtoValidator _sut;
    public CriarUsuarioDtoValidatorTests()
    {
        _sut = new CriarUsuarioDtoValidator();
    }

    [Fact(DisplayName = "TestValidate deve falhar quando nome do usuário é vazio")]
    [Trait("Categoria", "Usuario")]
    public void TestValidate_Falha_QuandoNomeVazio()
    {
        // Arrange
        var usuario = new CriarUsuarioDto
        {
            Nome = null
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.Nome);
    }


    [Fact(DisplayName = "TestValidate deve falhar quando sobre nome do usuário é vazio")]
    [Trait("Categoria", "Usuario")]
    public void TestValidate_Falha_QuandoSobreNomeVazio()
    {
        // Arrange
        var usuario = new CriarUsuarioDto
        {
            SobreNome = null
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.SobreNome);
    }

    [Fact(DisplayName = "TestValidate deve falhar quando nome do usuário nome maior que limite")]
    [Trait("Categoria", "Usuario")]
    public void TestValidate_Falha_QuandoNomeMaiorQueLimite()
    {
        // Arrange
        var usuario = new CriarUsuarioDto
        {
            Nome = new string(Enumerable.Repeat('a', 51).ToArray())
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    [Fact(DisplayName = "TestValidate deve falhar quando sobrenome do usuario maior que limite")]
    [Trait("Categoria", "Usuario")]
    public void TestValidate_Falha_QuandoSobreNomeMaiorQueLimite()
    {
        // Arrange
        var usuario = new CriarUsuarioDto
        {
            SobreNome = new string(Enumerable.Repeat('a', 51).ToArray())
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.SobreNome);
    }


    [Fact(DisplayName = "Validate usuario dados validos")]
    [Trait("Categoria", "Usuario")]
    public void TestValidate_ExecutaComSucesso_QuandoDadosValidos()
    {
        // Arrange
        var usuario = new CriarUsuarioDto
        {
            Nome = "Joao",
            SobreNome = "Mario"
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        Assert.True(validationResult.IsValid);
    }
}