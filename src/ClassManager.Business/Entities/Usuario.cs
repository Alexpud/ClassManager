using ClassManager.Business.Enums;
using Microsoft.AspNetCore.Identity;

namespace ClassManager.Business.Entities
{
    public class Usuario : IdentityUser
    {
        public string? Nome { get; set; }
        public string? SobreNome { get; set; }
        public TipoUsuario Tipo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
    }
}
