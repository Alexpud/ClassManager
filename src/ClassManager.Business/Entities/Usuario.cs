using ClassManager.Business.Enums;

namespace ClassManager.Business.Entities
{
    public class Usuario : BaseEntity
    {
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
