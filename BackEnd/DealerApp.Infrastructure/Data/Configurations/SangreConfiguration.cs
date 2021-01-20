using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class SangreConfiguration : IEntityTypeConfiguration<SangreCliente>
    {
        public void Configure(EntityTypeBuilder<SangreCliente> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__SangreCl__3845EF1724F70031");

            builder.HasIndex(e => e.TipoSangre)
                .HasName("UQ__SangreCl__20B0E4DC5F181B89")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID_Sangre")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.TipoSangre)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();
        }
    }
}