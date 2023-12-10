using ClassManager.Business.Entities;
using ClassManager.Business.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UserManager<Usuario> _userManager;

    public UsuarioRepository(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> Adicionar(Usuario usuario, string password) 
        => await _userManager.CreateAsync(usuario, password);

    public async Task<Usuario?> ObterPorId(Guid id) 
        => await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Usuario>> ObterTodos() 
        => await _userManager.Users.ToListAsync();
}
