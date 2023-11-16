namespace ClassManager.Business.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> CredenciaisSaoValidas(string username, string password);
        Task<string> GerarTokenAcesso(string userName);
    }
}