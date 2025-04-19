using prototipo1204.Models;

namespace prototipo1204.Repositorios.Interface
{
    public interface ILoginRepositorio
    {
        Cliente Login(string emailCliente, string senhaCliente);
    }
}
