using System.Threading.Tasks;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using DealerApp.Infrastructure.Data;

namespace DealerApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DealerContext _context;
        private readonly IRepository<Cliente> _clienteRepository;
        private readonly IRepository<Color> _colorRepository;
        private readonly IRepository<Combustible> _combustibleRepository;
        private readonly IRepository<Contrato> _contratoRepository;
        private readonly IRepository<Marca> _marcaRepository;
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Rol> _rolRepository;
        private readonly IRepository<SangreCliente> _sangreClienteRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<Vehiculo> _vehiculoRepository;
        private readonly ILoginRepository _loginRepository;

        public UnitOfWork(DealerContext context)
        {
            _context = context;
        }
        public IRepository<Cliente> ClienteRepository => _clienteRepository ?? new BaseRepository<Cliente>(_context);
        public IRepository<Color> ColorRepository => _colorRepository ?? new BaseRepository<Color>(_context);
        public IRepository<Combustible> CombustibleRepository => _combustibleRepository ?? new BaseRepository<Combustible>(_context);
        public IRepository<Contrato> ContratoRepository => _contratoRepository ?? new BaseRepository<Contrato>(_context);
        public IRepository<Marca> MarcaRepository => _marcaRepository ?? new BaseRepository<Marca>(_context);
        public IRepository<Modelo> ModeloRepository => _modeloRepository ?? new BaseRepository<Modelo>(_context);
        public IRepository<Rol> RolRepository => _rolRepository ?? new BaseRepository<Rol>(_context);
        public IRepository<SangreCliente> SangreClienteRepository => _sangreClienteRepository ?? new BaseRepository<SangreCliente>(_context);
        public IRepository<Usuario> UsuarioRepository => _usuarioRepository ?? new BaseRepository<Usuario>(_context);
        public IRepository<Vehiculo> VehiculoRepository => _vehiculoRepository ?? new BaseRepository<Vehiculo>(_context);

        public ILoginRepository LoginRepository => _loginRepository ?? new LoginRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}