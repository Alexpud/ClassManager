using ClassManager.Business.Dtos;
using ClassManager.Business.Entities;
using ClassManager.Business.Notifications;
using ClassManager.Business.Services.Concretos;
using ClassManager.Data.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassManager.Data.Authentication;

public class IdentityService : BaseService, IIdentityService
{
    private readonly SignInManager<Usuario> _siginManager;
    private readonly AuthenticationSettings _settings;
    public IdentityService(SignInManager<Usuario> siginManager, IOptions<AuthenticationSettings> options, INotificationServce notificationServce) : base(notificationServce)
    {
        _siginManager = siginManager;
        _settings = options.Value;
    }

    public async Task<string> Login(UsuarioLoginDto dto)
    {
        if (!await CredenciaisSaoValidas(dto.UserName, dto.Password))
        {
            Notificar("Login ou senha incorretas");
            return null;
        }

        return await GerarTokenAcesso(dto.UserName);
    }

    private async Task<bool> CredenciaisSaoValidas(string username, string password)
    {
        var signinResult = await _siginManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
        return signinResult.Succeeded;
    }

    private async Task<string> GerarTokenAcesso(string userName)
    {
        var usuario = await _siginManager.UserManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        if (usuario == null)
            return null;

        var claims = new Claim[]
        {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));

        var signingCredentials =
            new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(_settings.TokenExpirationInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

