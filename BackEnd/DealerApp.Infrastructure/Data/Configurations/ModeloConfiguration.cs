using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class ModeloConfiguration : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Modelo__813C2372E00E696B");

            builder.Property(e => e.Id)
                .HasColumnName("ID_Modelo")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}