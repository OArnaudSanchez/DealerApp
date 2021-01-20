using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Vehiculo__FEFD7E33CE211DE1");

            builder.HasIndex(e => e.Matricula)
                .HasName("UQ__Vehiculo__0FB9FB4FAD265A1F")
                .IsUnique();

            builder.HasIndex(e => e.Placa)
                .HasName("UQ__Vehiculo__8310F99DF07C8517")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID_Vehiculo");

            builder.Property(e => e.Lanzamiento)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(e => e.Precio).HasColumnType("money");

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(150);

                builder.Property(e => e.Puertas)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Foto)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.IdColor).HasColumnName("ID_Color");

            builder.Property(e => e.IdCombustible).HasColumnName("ID_Combustible");

            builder.Property(e => e.IdMarca).HasColumnName("ID_Marca");

            builder.Property(e => e.IdModelo).HasColumnName("ID_Modelo");

            builder.Property(e => e.Matricula).HasMaxLength(15);

            builder.Property(e => e.Placa).HasMaxLength(15);

            builder.HasOne(d => d.IdColorNavigation)
                .WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("FK_Color_Vehiculo");

            builder.HasOne(d => d.IdCombustibleNavigation)
                .WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.IdCombustible)
                .HasConstraintName("FK_Combustible_Vehiculo");

            builder.HasOne(d => d.IdMarcaNavigation)
                .WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK_Marca_Vehiculo");

            builder.HasOne(d => d.IdModeloNavigation)
                .WithMany(p => p.Vehiculo)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("FK_Modelo_Vehiculo");
        }
    }
}