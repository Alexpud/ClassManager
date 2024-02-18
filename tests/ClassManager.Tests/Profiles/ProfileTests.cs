using AutoMapper;
using ClassManager.Business.Profiles;

namespace ClassManager.Business.Tests.Profiles;

public class ProfileTests
{
    [Fact(DisplayName = "UsuarioProfile deve ser valido")]
    [Trait("Categoria", "Mapper")]
    public void UsuarioProfile_Deve_TerOsMapeamentos_Validos()
    {
        var configuration = new MapperConfiguration(p => p.AddProfile(new UsuarioProfile()));
        configuration.AssertConfigurationIsValid();
    }

    [Fact(DisplayName = "CursoProfile deve ser valido")]
    [Trait("Categoria", "Mapper")]
    public void CursoProfile_Deve_TerOsMapeamentos_Validos()
    {
        var configuration = new MapperConfiguration(p => p.AddProfile(new CursoProfile()));
        configuration.AssertConfigurationIsValid();
    }
}