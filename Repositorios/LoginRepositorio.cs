using prototipo1204.Data;
using prototipo1204.Models;
using prototipo1204.Repositorios.Interface;
using System.Linq;

namespace prototipo1204.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private readonly oceanidDBContext _context;

        public LoginRepositorio(oceanidDBContext context)
        {
            _context = context;
        }

        public Cliente Login(string emailCliente, string senhaCliente)
        {
            return _context.Clientes
                .FirstOrDefault(c => c.emailCliente == emailCliente && c.senhaCliente == senhaCliente);
        }
    }
}
