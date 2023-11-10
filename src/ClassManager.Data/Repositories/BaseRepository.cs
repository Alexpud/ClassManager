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

    public TEntity Adicionar(TEntity entidade)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> ObterPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> ObterTodos()
    {
        throw new NotImplementedException();
    }

    public Task SaveChanges()
    {
        throw new NotImplementedException();
    }
}