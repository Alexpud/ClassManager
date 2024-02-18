using ClassManager.Business.Dtos.Authentication;
using ClassManager.Business.Entities;
using ClassManager.Data.Options;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassManager.Data.Authentication;

public class IdentityService
{
    private readonly SignInManager<Usuario> _siginManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly AuthenticationSettings _settings;
    public IdentityService(
        SignInManager<Usuario> siginManager,
        IOptions<AuthenticationSettings> options,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _siginManager = siginManager;
        _roleManager = roleManager;
        _settings = options.Value;
    }

    public async Task<Result<LoginResponseDto>> Login(LoginDto dto)
    {
        var result = new Result<LoginResponseDto>();
        if (!await CredenciaisSaoValidas(dto.UserName, dto.Password))
            return result.WithError("Credenciais invalidas");

        var tokenJwt = await GerarTokenAcesso(dto.UserName);
        return new LoginResponseDto
        {
            Token = tokenJwt,
        };
    }

    private async Task<bool> CredenciaisSaoValidas(string username, string password)
    {
        var signinResult = await _siginManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
        return signinResult.Succeeded;
    }

    private async Task<string> GerarTokenAcesso(string userName)
    {
        var usuario = await _siginManager.UserManager.Users.FirstOrDefaultAsync(user => user.UserName == userName);
        var claims = new Claim[]
        {
            new Claim("username", usuario.UserName),
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Tipo.ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
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

    public async Task<Result> CriarRole(string nome)
    {
        var result = new Result();
        var identityResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(nome));
        if (!identityResult.Succeeded)
            return result.WithErrors(identityResult.Errors.Select(p => new Error(p.Description)));
        return result;
    }

}

