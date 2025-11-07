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

            return emprestimosAtivos < 3;
        }
    }
}
