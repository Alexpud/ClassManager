using Microsoft.AspNetCore.Identity;

namespace ClassManager.Api.Identity;

public class CustomIdentityErrors : IdentityErrorDescriber
{
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = "DuplicateUserName",
            Description = $"Já existe um usuário com o userName {userName}."
        };
    }

    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError
        {
            Code = "InvalidUserName",
            Description = $"O username {userName} é invalido"
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError
        {
            Code = "DuplicateRoleName",
            Description = $"O role {role} já existe"
        };
    }
}