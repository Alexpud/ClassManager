
using ClassManager.Business.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClassManager.Business.Repositories;

public interface IUsuarioRepository
{
    Task<IdentityResult> Adicionar(Usuario usuario, string? password);
    Task<Usuario?> ObterPorId(Guid id);
    Task<IEnumerable<Usuario>> ObterTodos();
}
