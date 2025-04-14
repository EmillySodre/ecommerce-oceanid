namespace prototipo1204.Models
{
    public class EnderecoCliente
    {
        public int idEndCliente { get ; set; }

        public int idCliente { get; set; }
        public Cliente cliente { get; set; }
        public int idEndereco { get; set; }
      public Endereco endereco { get; set; }


    }
}
