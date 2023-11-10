using ClassManager.Business.Entities;

namespace ClassManager.Business.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    TEntity Adicionar(TEntity entidade);
    Task<TEntity> ObterPorId(Guid id);
    Task<IEnumerable<TEntity>> ObterTodos();
    Task SaveChanges();
}