using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Multa> Multas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("BibliotecaDB");
    }
}
