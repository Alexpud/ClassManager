namespace ClassManager.Business.Notifications;

public class Notification
{
    public string Mensagem { get; set; }
    public Notification(string mensagem)
    {
        Mensagem = mensagem;
    }
}