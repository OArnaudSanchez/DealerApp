using System;
using DealerApp.Core.Entities;
using DealerApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Rol__202AD22027A72CDB");

            builder.HasIndex(e => e.TipoRol)
                .HasName("UQ__Rol__C666E1AC37D17D0E")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID_Rol")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Creacion)
                .HasMaxLength(16)
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.TipoRol)
            .HasConversion(x => x.ToString(), x => (RolType)Enum.Parse(typeof(RolType), x))
            .HasMaxLength(15);
        }
    }
}