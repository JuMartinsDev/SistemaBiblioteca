using BibliotecaApp.Data;
using BibliotecaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Services
{
    public class RelatorioService
    {
        private readonly BibliotecaContext _context;

        public RelatorioService(BibliotecaContext context)
        {
            _context = context;
        }

        public IEnumerable<object> LivrosMaisEmprestados()
        {
            return _context.Emprestimos
                .GroupBy(e => e.ISBN)
                .Select(g => new { ISBN = g.Key, TotalEmprestimos = g.Count() })
                .OrderByDescending(x => x.TotalEmprestimos)
                .ToList();
        }

        public IEnumerable<object> UsuariosComMaisEmprestimos()
        {
            return _context.Emprestimos
                .GroupBy(e => e.UsuarioId)
                .Select(g => new { UsuarioId = g.Key, TotalEmprestimos = g.Count() })
                .OrderByDescending(x => x.TotalEmprestimos)
                .ToList();
        }

        public IEnumerable<Emprestimo> EmprestimosEmAtraso()
        {
            return _context.Emprestimos
                .Where(e => e.Status == StatusEmprestimo.ATRASADO)
                .ToList();
        }
    }
}
