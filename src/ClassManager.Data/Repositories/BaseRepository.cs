using ClassManager.Business.Entities;
using ClassManager.Business.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data.Repositories;

public abstract class BaseRepository<TEntity>  : IBaseRepository<TEntity>  where TEntity : BaseEntity
{
    private readonly ClassManagerDbContext _dbContext;
    private DbSet<TEntity> _set;

    public BaseRepository(ClassManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<TEntity>();
    }

    public void Adicionar(TEntity entidade) 
        => _set.Add(entidade);

    public async Task<TEntity?> ObterPorId(Guid id)
        => await _set.FindAsync(id);

    public async Task<IEnumerable<TEntity>> ObterTodos()
        => await _set.ToListAsync();

    public async Task SaveChanges()
        => await _dbContext.SaveChangesAsync();
}