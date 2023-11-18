using ClassManager.Business.Entities;
using ClassManager.Business.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Usuario> ObterPorId(Guid id)
    {
        return await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);
        throw new NotImplementedException();
    }
}
