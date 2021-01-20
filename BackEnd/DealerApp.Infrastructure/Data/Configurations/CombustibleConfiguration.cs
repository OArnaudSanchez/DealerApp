using System;
using DealerApp.Core.Entities;
using DealerApp.Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class CombustibleConfiguration : IEntityTypeConfiguration<Combustible>
    {
        public void Configure(EntityTypeBuilder<Combustible> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Combusti__ED9A38958920292C");

            builder.HasIndex(e => e.TipoCombustible)
                .HasName("UQ__Combusti__9ADF717367F89EAC")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID_Combustible")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.TipoCombustible)
                .IsRequired()
                .HasConversion(x => x.ToString(), x => (CombustibleType)Enum.Parse(typeof(CombustibleType), x))
                .HasMaxLength(10);
        }
    }
}