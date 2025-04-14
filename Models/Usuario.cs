namespace prototipo1204.Models
{
    public class Usuario
    {
        public int idUser { get; set; }
        public string? emailUser { get; set; }
        public string? senhaUser { get; set; }
        public string? telefoneUser { get; set; }
        public DateTime? datacad_User { get; set; }

        public List<Adm> Adms { get; set; }

        public List<Cliente> Clientes { get; set; }
    }
}
