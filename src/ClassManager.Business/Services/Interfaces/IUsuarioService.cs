using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;

namespace ClassManager.Business.Services.Interfaces;

public interface IUsuarioService
{
    Task<Usuario> Adicionar(UsuarioCriacaoDto usuario);
    Task<Usuario> ObterPorId(Guid id);
    Task<IEnumerable<Usuario>> ObterTodos();
}