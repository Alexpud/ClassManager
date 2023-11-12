using ClassManager.Business.Entities;

namespace ClassManager.Business.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    void Adicionar(TEntity entidade);
    Task<TEntity?> ObterPorId(Guid id);
    Task<IEnumerable<TEntity>> ObterTodos();
    void Remover(Guid id);
    Task SaveChanges();
}