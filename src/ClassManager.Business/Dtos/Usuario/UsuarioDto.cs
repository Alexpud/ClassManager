using ClassManager.Business.Enums;

namespace ClassManager.Business.Dtos.Usuario;

public class UsuarioDto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public TipoUsuario Tipo { get; set; }
}
