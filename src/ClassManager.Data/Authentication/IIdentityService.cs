using ClassManager.Business.Dtos;

namespace ClassManager.Data.Authentication;

public interface IIdentityService
{
    Task<string?> Login(UsuarioLoginDto dto);
}