using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealerApp.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK__Usuario__DE4431C57AEA07D4");

            builder.HasIndex(e => e.Email)
                .HasName("UQ__Usuario__A9D105348E447530")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("ID_Usuario")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(e => e.Contrasena).HasMaxLength(15);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Estatus).HasDefaultValueSql("((1))");

            builder.Property(e => e.Creacion)
                .HasMaxLength(16)
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Foto)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.IdRol).HasColumnName("ID_Rol");

            builder.Property(e => e.IdSangre).HasColumnName("ID_Sangre");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(45);

            builder.HasOne(d => d.IdSangreNavigation)
                .WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdSangre)
                .HasConstraintName("FK_Usuario_Sangre");
        }
    }
}