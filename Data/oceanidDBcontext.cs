using Microsoft.EntityFrameworkCore;
using prototipo1204.Models;

namespace prototipo1204.Data
{
    public class oceanidDBContext : DbContext
    {
       
            public oceanidDBContext(DbContextOptions<oceanidDBContext> options) : base(options) { }

            public DbSet<Endereco> Enderecos { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Adm> Adms { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<EnderecoCliente> EnderecoClientes { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configuração das tabelas
                modelBuilder.Entity<Endereco>().ToTable("tbEndereco");
                modelBuilder.Entity<Usuario>().ToTable("tbUsuario");
                modelBuilder.Entity<Adm>().ToTable("tbAdm");
                modelBuilder.Entity<Cliente>().ToTable("tbCliente");
                modelBuilder.Entity<EnderecoCliente>().ToTable("tbEnderecoCliente");

                // Configuração das chaves primárias
                modelBuilder.Entity<Endereco>().HasKey(e => e.idEnd);
                modelBuilder.Entity<Usuario>().HasKey(u => u.idUser);
                modelBuilder.Entity<Adm>().HasKey(a => a.idAdm);
                modelBuilder.Entity<Cliente>().HasKey(c => c.idCliente);
                modelBuilder.Entity<EnderecoCliente>().HasKey(ec => ec.idEndCliente);

                // Relacionamento N:N entre Cliente e Endereco
                modelBuilder.Entity<EnderecoCliente>()
                    .HasOne(ec => ec.cliente)
                    .WithMany(c => c.EnderecoClientes)
                    .HasForeignKey(ec => ec.idCliente);

                modelBuilder.Entity<EnderecoCliente>()
                    .HasOne(ec => ec.endereco)
                    .WithMany(e => e.EnderecoClientes)
                    .HasForeignKey(ec => ec.idEndereco);

                // Relacionamento Adm-Usuario
                modelBuilder.Entity<Adm>()
                    .HasOne(a => a.usuario)
                    .WithMany(u => u.Adms)
                    .HasForeignKey(a => a.idUser)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relacionamento Cliente-Usuario
                modelBuilder.Entity<Cliente>()
                    .HasOne(c => c.usuario)
                    .WithMany(u => u.Clientes)
                    .HasForeignKey(c => c.idUser)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configurações adicionais para melhor desempenho
                modelBuilder.Entity<Usuario>()
                    .HasIndex(u => u.emailUser)
                    .IsUnique();

                modelBuilder.Entity<Cliente>()
                    .HasIndex(c => c.cpf)
                    .IsUnique();
            }
        
    }
}
