using ClassManager.Business.Enums;

namespace ClassManager.Business.Dtos.Usuario;

public class CriarUsuarioDto
{
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public TipoUsuario TipoUsuario { get; set; }
}