using DealerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DealerApp.Infrastructure.Data
{
    public partial class DealerContext : DbContext
    {
        public DealerContext(DbContextOptions<DealerContext> options)
            : base(options)
        {
        }   

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Combustible> Combustible { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<SangreCliente> SangreCliente { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DealerContext).Assembly);
        }
    }
}
