namespace ClassManager.Business.Notifications;

public interface INotificationServce
{
    void Handle(Notification notification);
    void Handle(string mensagem);
    bool TemNotificacoes();
    List<Notification> ObterNotificacoes();
}