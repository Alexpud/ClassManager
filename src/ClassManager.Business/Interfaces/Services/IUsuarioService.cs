using ClassManager.Business.Dtos.Usuario;
using ClassManager.Business.Entities;

namespace ClassManager.Business.Interfaces.Services;

public interface IUsuarioService
{
    Task<Usuario?> Criar(UsuarioCriacaoDto usuario);
    Task<UsuarioDto?> ObterDadosResumidosPorId(Guid id);
    Task<IEnumerable<UsuarioDto>> ObterTodos();
    //Task Remover(Guid id);
}