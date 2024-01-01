using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Enums;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Notifications;
using FluentValidation;

namespace ClassManager.Business.Services;

public class CursoService : BaseService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IValidator<CriarCursoDto> _criarCursoDtoValidator;

    public CursoService(
        INotificationServce notificationServce,
        IUsuarioRepository usuarioRepository,
        IValidator<CriarCursoDto> validator) : base(notificationServce)
    {
        _usuarioRepository = usuarioRepository;
        _criarCursoDtoValidator = validator;
    }

    public async Task<CursoDto> CriarCurso(CriarCursoDto dto)
    {
        if (!Validar(_criarCursoDtoValidator, dto))
            return null;

        if (dto.ProfessorId == Guid.Empty)
            _notificationService.Handle("Não pode criar curso sem professor");

        var usuario = await _usuarioRepository.ObterPorId(dto.ProfessorId);
        if (usuario == null || usuario.Tipo != TipoUsuario.Professor)
            _notificationService.Handle("Professor não foi encontrado");
        // Verificar se o curso é valido
        // Possui nome?
        // O professor existe?
        return new CursoDto();
    }
}
