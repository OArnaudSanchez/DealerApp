using System;
using DealerApp.Core.Entities;
using DealerApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Cliente__E005FBFF6AC6A440");

            builder.HasIndex(e => e.Dni)
                .HasName("UQ__Cliente__C03085758361AA8A")
                .IsUnique();

            builder.HasIndex(e => e.Email)
                .HasName("UQ__Cliente__A9D10534810E727D")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID_Cliente");

            builder.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Dni).HasMaxLength(11);

            builder.Property(e => e.Email).HasMaxLength(30);
            builder.Property(e => e.Telefono).HasMaxLength(10);


            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Foto)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Sexo)
            .IsRequired()
            .HasMaxLength(1)
            .HasConversion(x => x.ToString(), x => (SexoEnum)Enum.Parse(typeof(SexoEnum), x));

            builder.Property(e => e.IdRol)
                .HasColumnName("ID_Rol")
                .HasDefaultValueSql("((3))");

            builder.Property(e => e.IdSangre).HasColumnName("ID_Sangre");

            builder.Property(e => e.Nacimiento)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45);

            builder.HasOne(d => d.IdSangreNavigation)
                .WithMany(p => p.Cliente)
                .HasForeignKey(d => d.IdSangre)
                .HasConstraintName("FK_Cliente_Sangre");
        }
    }
}