using ClassManager.Business.Entities;
using ClassManager.Business.Validators.Entities;
using FluentValidation.TestHelper;

namespace ClassManager.Business.Tests.Validators.Entities;

public class UsuarioValidatorTests
{
    private readonly UsuarioValidator _sut;
    public UsuarioValidatorTests()
    {
        _sut = new UsuarioValidator();
    }

    [Fact(DisplayName = "Validate Usuario nome vazio")]
    [Trait("Categoria", "Validators")]
    public void Validate_NomeVazio_DeveRetornarComErros()
    {
        // Arrange
        var usuario = new Usuario
        {
            Nome = null
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    
    [Fact(DisplayName = "Validate Usuario sobrenome vazio")]
    [Trait("Categoria", "Validators")]
    public void Validate_SobreNomeVazio_DeveRetornarComErros()
    {
        // Arrange
        var usuario = new Usuario
        {
            SobreNome = null
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.SobreNome);
    }

    [Fact(DisplayName = "Validate Usuario nome maior que limite")]
    [Trait("Categoria", "Validator")]
    public void Validate_DeveTerErro_NomeMaiorQueLimite()
    {
        // Arrange
        var usuario = new Usuario
        {
            Nome = new string(Enumerable.Repeat('a', 51).ToArray())
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    [Fact(DisplayName = "Validate Usuario sobrenome maior que limte")]
    [Trait("Categoria", "Validator")]
    public void Validate_DeveTerErro_SobreNomeMaiorQueLimite()
    {
        // Arrange
        var usuario = new Usuario
        {
            SobreNome = new string(Enumerable.Repeat('a', 51).ToArray())
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.SobreNome);
    }


    [Fact(DisplayName = "Validate usuario dados validos")]
    [Trait("Categoria", "Validator")]
    public void Validate_DadosValidos_DeveRetornarSemFalhas()
    {
        // Arrange
        var usuario = new Usuario
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