using ClassManager.Business.Entities;
using ClassManager.Business.Repositories;

namespace ClassManager.Data.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ClassManagerDbContext dbContext) : base(dbContext) {  }
}
