using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Enums;
using ClassManager.Business.Interfaces.Repositories;
using ClassManager.Business.Notifications;

namespace ClassManager.Business.Services;

public class CursoService : BaseService
{
    private readonly IUsuarioRepository _usuarioRepository;
    public CursoService(
        INotificationServce notificationServce,
        IUsuarioRepository usuarioRepository) : base(notificationServce)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<CursoDto> CriarCurso(CriarCursoDto dto)
    {
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
