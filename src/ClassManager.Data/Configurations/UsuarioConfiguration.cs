using ClassManager.Business.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassManager.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(50);
            builder.Property(p => p.SobreNome).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Tipo).IsRequired().HasConversion<string>();
            builder.Property(p => p.Login).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Senha).IsRequired().HasMaxLength(255);
        }
    }
}