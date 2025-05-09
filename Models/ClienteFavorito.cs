namespace prototipo1204.Models
{
    public class ClienteFavorito
    {
        public int IdClienteFav { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }

        public int IdProd { get; set; }
        public Produto Produto { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
