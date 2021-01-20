using System;
using DealerApp.Core.Entities;
using DealerApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Contrato__B16B9C19668F2694");

            builder.Property(e => e.Id).HasColumnName("ID_Contrato");

            builder.Property(e => e.Concepto)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(e => e.Fecha)
                .HasMaxLength(16)
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Estatus)
            .IsRequired()
            .HasConversion(x => x.ToString(), x => (EstatusContratoType)Enum.Parse(typeof(EstatusContratoType), x));

            builder.Property(e => e.IdCliente).HasColumnName("ID_Cliente");

            builder.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");

            builder.Property(e => e.IdVehiculo).HasColumnName("ID_Vehiculo");

            builder.HasOne(d => d.IdClienteNavigation)
                .WithMany(p => p.Contrato)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Cliente_Contrato");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Contrato)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Usuario_Contrato");

            builder.HasOne(d => d.IdVehiculoNavigation)
                .WithMany(p => p.Contrato)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK_Vehiculo_Contrato");
        }
    }
}