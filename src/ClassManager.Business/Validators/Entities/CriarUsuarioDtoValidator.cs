using ClassManager.Business.Dtos.Usuario;
using FluentValidation;

namespace ClassManager.Business.Validators.Entities;

public class CriarUsuarioDtoValidator : AbstractValidator<CriarUsuarioDto>
{
    public CriarUsuarioDtoValidator()
    {
        RuleFor(p => p.Nome)
            .Must(p => !string.IsNullOrEmpty(p) && !string.IsNullOrWhiteSpace(p))
            .WithMessage("{PropertyName} não pode ser vazio ou nulo")
            .MaximumLength(50)
            .WithMessage("O tamanho máximo do campo {PropertyName} deve se de {MaxLength} caracteres");

        RuleFor(p => p.SobreNome)
            .Must(p => !string.IsNullOrEmpty(p))
            .WithMessage("{PropertyName} não pode ser vazio ou nulo")
            .MaximumLength(50)
            .WithMessage("O tamanho máximo do campo {PropertyName} deve se de {MaxLength} caracteres");
    }
}