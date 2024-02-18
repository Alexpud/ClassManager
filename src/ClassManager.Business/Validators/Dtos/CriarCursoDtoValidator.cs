using ClassManager.Business.Dtos.Curso;
using FluentValidation;

public class CriarCursoDtoValidator : AbstractValidator<CriarCursoDto>
{
    public CriarCursoDtoValidator()
    {
        RuleFor(p => p.Nome)
            .Must(p => !string.IsNullOrEmpty(p) && !string.IsNullOrWhiteSpace(p))
            .WithMessage("Não pode criar curso sem nome");
    }
}