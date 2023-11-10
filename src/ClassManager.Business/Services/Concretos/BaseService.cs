using ClassManager.Business.Entities;
using FluentValidation;

namespace ClassManager.Business.Services.Concretos;

public abstract class BaseService<TEntity> where TEntity : BaseEntity
{
    protected bool Validar(IValidator<TEntity> validator, TEntity entidade)
    {
        var validation = validator.Validate(entidade);
        if (validation.IsValid)
            return true;
        return false;
    }
}
