using Microsoft.EntityFrameworkCore;
using prototipo1204.Models;

namespace prototipo1204.Data
{
    public class oceanidDBContext : DbContext
    {
       
            public oceanidDBContext(DbContextOptions<oceanidDBContext> options) : base(options) { }

            public DbSet<Endereco> Enderecos { get; set; }
            public DbSet<Adm> Adms { get; set; }
            public DbSet<Cliente> Clientes { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Configuração das tabelas
                modelBuilder.Entity<Endereco>().ToTable("tbEndereco");
                modelBuilder.Entity<Adm>().ToTable("tbAdm");
                modelBuilder.Entity<Cliente>().ToTable("tbCliente");

                // Configuração das chaves primárias
                modelBuilder.Entity<Endereco>().HasKey(e => e.idEnd);
                modelBuilder.Entity<Adm>().HasKey(a => a.idAdm);
                modelBuilder.Entity<Cliente>().HasKey(c => c.idCliente);

                // Relacionamento 1:N entre Cliente e Endereco
                modelBuilder.Entity<Cliente>()
                    .HasOne(c => c.endereco)
                    .WithMany(e => e.clientes)
                    .HasForeignKey(c => c.idEnd)
                    .IsRequired(false);


        }
        
    }
}
