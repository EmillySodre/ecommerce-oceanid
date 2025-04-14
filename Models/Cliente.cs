namespace prototipo1204.Models
{ 
    public class Cliente
    {
        public int idCliente { get; set; }
        public string? cpf { get; set; }
        public string? nomeCompleto { get; set; }
        public DateOnly? dataNasc { get; set; }
        public int idUser { get; set; }
        public Usuario usuario { get; set; }
        public List<EnderecoCliente> EnderecoClientes { get; set; }
    }
}
