using ClassManager.Business.Dtos.Authentication;

namespace ClassManager.Data.Authentication;

public interface IIdentityService
{
    Task CriarRole(string nome);
    Task<LoginResponseDto> Login(LoginDto dto);
}