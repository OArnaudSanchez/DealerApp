using System;
using System.Threading.Tasks;
using DealerApp.Core.Entities;

namespace DealerApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cliente> ClienteRepository { get; }
        IRepository<Color> ColorRepository { get; }
        IRepository<Combustible> CombustibleRepository { get; }
        IRepository<Contrato> ContratoRepository { get; }
        IRepository<Marca> MarcaRepository { get; }
        IRepository<Modelo> ModeloRepository { get; }
        IRepository<Rol> RolRepository { get; }
        IRepository<SangreCliente> SangreClienteRepository { get; }
        IRepository<Usuario> UsuarioRepository { get; }
        IRepository<Vehiculo> VehiculoRepository { get; }
        ILoginRepository LoginRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}