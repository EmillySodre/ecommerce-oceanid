using prototipo1204.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace prototipo1204.Repositorios
{
    public class DashboardRepositorio
    {
        private readonly oceanidDBContext _context;

        public DashboardRepositorio(oceanidDBContext context)
        {
            _context = context;
        }


        // Método para calcular o total de vendas do mês
        public decimal VendasMes()
        {
            var vendasDoMes = _context.Pedido
                .Where(p => p.dataPed.Month == DateTime.Now.Month && p.dataPed.Year == DateTime.Now.Year)
                .Sum(p => p.totalPed);
            return vendasDoMes;
        }
    }
}