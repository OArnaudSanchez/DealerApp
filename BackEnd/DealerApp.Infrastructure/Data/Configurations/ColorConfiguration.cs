using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasKey(e => e.Id)
                    .HasName("PK__Color__6E7EBE7C628FA872");

            builder.HasIndex(e => e.Nombre)
                .HasName("UQ__Color__75E3EFCF5357A915")
                .IsUnique();
            builder.Property(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("ID_Color")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}