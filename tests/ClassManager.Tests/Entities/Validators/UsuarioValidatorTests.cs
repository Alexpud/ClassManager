using ClassManager.Business.Entities;
using ClassManager.Business.Entities.Validators;
using FluentValidation.TestHelper;

namespace ClassManager.Business.Tests.Entities.Validators;

public class UsuarioValidatorTests
{
    private readonly UsuarioValidator _sut;
    public UsuarioValidatorTests()
    {
        _sut = new UsuarioValidator();
    }

    [Fact]
    public void Validate_Deve_TerErro_ParaNome_Quando_ForNulo()
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

    [Fact]
    public void Validate_Deve_TerErro_ParaNome_Quando_ForVazio()
    {
        // Arrange
        var usuario = new Usuario
        {
            Nome = string.Empty
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldHaveValidationErrorFor(p => p.Nome);
    }

    [Fact]
    public void Validate_Deve_TerErro_ParaNome_Quando_ForMaiorQue_50()
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

    [Theory]
    [InlineData("Joao")]
    [InlineData("Joao Pedro")]
    [InlineData(" Pedro")]
    public void Validate_NaoDeveTerErro_ParaNome_Quando_ForValido(string nome)
    {
        // Arrange
        var usuario = new Usuario
        {
            Nome = nome
        };

        // Act
        var validationResult = _sut.TestValidate(usuario);

        // Assert
        validationResult.ShouldNotHaveValidationErrorFor(p => p.Nome);
    }
}