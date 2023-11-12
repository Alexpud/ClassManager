using ClassManager.Business.Enums;

namespace ClassManager.Business.Dtos;

public class UsuarioCriacaoDto
{
    public string? Nome { get; set; }
    public string? SobreNome { get; set; }
    public string? Login { get; set; }
    public string? Senha { get; set; }
    public TipoUsuario TipoUsuario { get; set; }
}