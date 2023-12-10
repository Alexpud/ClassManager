using AutoMapper;
using ClassManager.Business.Profiles;

namespace ClassManager.Business.Tests.Profiles;


public class UsuarioProfileTests
{
    [Fact]
    public void UsuarioProfile_Deve_TerOsMapeamentos_Validos()
    {
        var configuration = new MapperConfiguration(p => p.AddProfile(new UsuarioProfile()));
        configuration.AssertConfigurationIsValid();
    }
}