namespace ClassManager.Business.Notifications;

public class NotificationService : INotificationServce
{
    private readonly List<Notification> _notifications = new();

    public void Handle(Notification notificacao)
        => _notifications.Add(notificacao);

    public void Handle(string mensagem)
        => _notifications.Add(new Notification(mensagem));

    public bool TemNotificacoes()
        => _notifications.Any();
    
    public List<Notification> ObterNotificacoes()
        => _notifications;
}


public interface INotificationServce
{
    void Handle(Notification notification);
    void Handle(string mensagem);
    bool TemNotificacoes();
    List<Notification> ObterNotificacoes();
}