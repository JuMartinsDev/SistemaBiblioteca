using BibliotecaApp.Data;
using BibliotecaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Services
{
    public class LivroService
    {
        private readonly BibliotecaContext _context;

        public LivroService(BibliotecaContext context)
        {
            _context = context;
        }

        public void CadastrarLivro(Livro livro)
        {
            livro.DataCadastro = DateTime.Now;
            _context.Livros.Add(livro);
            _context.SaveChanges();
        }

        public void AtualizarStatus(string isbn, StatusLivro novoStatus)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.ISBN == isbn);
            if (livro == null) throw new Exception("Livro não encontrado.");

            livro.Status = novoStatus;
            _context.SaveChanges();
        }

        public bool EstaDisponivel(string isbn)
        {
            var livro = _context.Livros.FirstOrDefault(l => l.ISBN == isbn);
            return livro != null && livro.Status == StatusLivro.DISPONIVEL;
        }
    }
}
