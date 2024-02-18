using ClassManager.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassManager.Data.Context.Configurations;

public class SemestreTypeConfiguration : IEntityTypeConfiguration<Semestre>
{
    public void Configure(EntityTypeBuilder<Semestre> builder)
    {
        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(50);
    }
}
