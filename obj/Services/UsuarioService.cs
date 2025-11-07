using BibliotecaApp.Data;
using BibliotecaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Services
{
    public class UsuarioService
    {
        private readonly BibliotecaContext _context;

        public UsuarioService(BibliotecaContext context)
        {
            _context = context;
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public bool PodeEmprestar(int usuarioId)
        {
            var emprestimosAtivos = _context.Emprestimos
                .Count(e => e.UsuarioId == usuarioId && e.Status == StatusEmprestimo.ATIVO);

            var temMultaPendente = _context.Multas
                .Any(m => m.Emprestimo.UsuarioId == usuarioId && m.Status == StatusMulta.PENDENTE);

            return emprestimosAtivos < 3 && !temMultaPendente;
        }
    }
}
