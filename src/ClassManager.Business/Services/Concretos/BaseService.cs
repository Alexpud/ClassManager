using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using FluentValidation;

namespace ClassManager.Business.Services.Concretos;

public abstract class BaseService
{
    protected readonly INotificationServce _notificationService;
    protected BaseService(INotificationServce notificationServce)
    {
        _notificationService = notificationServce;
    }

    protected bool Validar<TEntity>(IValidator<TEntity> validator, TEntity entidade)
    {
        var validation = validator.Validate(entidade);
        if (validation.IsValid)
            return true;

        foreach(var error in validation.Errors)
            _notificationService.Handle(error.ErrorMessage);

        return false;
    }

    protected void Notificar(IEnumerable<string> messages)
    {
        foreach(var message in messages)
            _notificationService.Handle(message);

    }
}