using ClassManager.Business.Entities;
using ClassManager.Business.Repositories;
using ClassManager.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassManager.Data.Repositories;

public abstract class BaseRepository<TEntity>  : IBaseRepository<TEntity>  where TEntity : BaseEntity, new()
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

    public void Remover(Guid id)
        => _set.Remove(new TEntity{Id = id});

    public async Task SaveChanges()
        => await _dbContext.SaveChangesAsync();
}