
using ClassManager.Business.Entities;

namespace ClassManager.Business.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    TEntity Adicionar(TEntity entidade);
    Task<TEntity> ObterPorId(Guid id);
    Task<IEnumerable<TEntity>> ObterTodos();
    Task SaveChanges();
}