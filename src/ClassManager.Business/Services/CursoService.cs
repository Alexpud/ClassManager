using ClassManager.Business.Dtos.Curso;
using ClassManager.Business.Notifications;

namespace ClassManager.Business.Services;

public class CursoService : BaseService
{
    public CursoService(INotificationServce notificationServce) : base(notificationServce)
    {
    }

    public void AdicionarCurso(CriarCursoDto dto)
    {
        // Verificar se o curso é valido
            // Possui nome?
            // O professor existe?
        
    }
}
