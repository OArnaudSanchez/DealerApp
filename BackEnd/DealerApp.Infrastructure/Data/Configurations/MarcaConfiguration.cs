using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Marca__9B8F8DB2F53E5259");

            builder.HasIndex(e => e.Nombre)
                .HasName("UQ__Marca__75E3EFCF610771F6")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID_Marca")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Lanzamiento)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Foto)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Nombre).HasMaxLength(30);
        }
    }
}