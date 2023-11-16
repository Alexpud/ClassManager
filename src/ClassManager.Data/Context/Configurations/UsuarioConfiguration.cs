using ClassManager.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassManager.Data.Context.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Nome).IsRequired().HasMaxLength(50);
            builder.Property(p => p.SobreNome).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Tipo).IsRequired().HasConversion<string>();
            builder.Property(p => p.CriadoEm).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}