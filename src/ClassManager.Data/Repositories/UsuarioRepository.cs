using ClassManager.Business.Entities;
using ClassManager.Business.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ClassManager.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<Usuario> _userManager;

    public UsuarioRepository(UserManager<Usuario> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<IdentityResult> Adicionar(Usuario usuario, string password)
    {
        return await _userManager.CreateAsync(usuario, password);
    }
}
