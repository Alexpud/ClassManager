using ClassManager.Business.Entities;
using FluentValidation;

namespace ClassManager.Business.Services.Concretos;

public abstract class BaseService
{
    protected bool Validar<TEntity>(IValidator<TEntity> validator, TEntity entidade) where TEntity : BaseEntity
    {
        var validation = validator.Validate(entidade);
        if (validation.IsValid)
            return true;
        return false;
    }
}