using BibliotecaApp.Data;
using BibliotecaApp.Models;
using BibliotecaApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApp.Services
{
    public class EmprestimoService
    {
        private readonly BibliotecaContext _context;
        private readonly LivroService _livroService;
        private readonly UsuarioService _usuarioService;

        public EmprestimoService(BibliotecaContext context, LivroService livroService, UsuarioService usuarioService)
        {
            _context = context;
            _livroService = livroService;
            _usuarioService = usuarioService;
        }

        public void RegistrarEmprestimo(int usuarioId, string isbn)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            if (usuario == null)
                throw new NegocioException("Usuário não encontrado.");

            var multaPendente = _context.Multas.Any(m =>
                m.Emprestimo.UsuarioId == usuarioId && m.Status == StatusMulta.PENDENTE);
            if (multaPendente)
                throw new NegocioException("Usuário possui multa pendente e não pode realizar novos empréstimos.");

            if (!_usuarioService.PodeEmprestar(usuarioId))
                throw new NegocioException("Usuário já possui 3 empréstimos ativos.");

            if (!_livroService.EstaDisponivel(isbn))
                throw new NegocioException("Livro não está disponível para empréstimo.");

            var emprestimo = new Emprestimo
            {
                UsuarioId = usuarioId,
                ISBN = isbn,
                DataEmprestimo = DateTime.Now,
                Status = StatusEmprestimo.ATIVO
            };

            emprestimo.DataPrevistaDevolucao = usuario.Tipo == TipoUsuario.PROFESSOR
                ? DateTime.Now.AddDays(15)
                : DateTime.Now.AddDays(7);

            _context.Emprestimos.Add(emprestimo);
            _livroService.AtualizarStatus(isbn, StatusLivro.EMPRESTADO);
            _context.SaveChanges();
        }

        public void RegistrarDevolucao(int emprestimoId)
        {
            var emprestimo = _context.Emprestimos
                .Include(e => e.Usuario)
                .FirstOrDefault(e => e.Id == emprestimoId);

            if (emprestimo == null)
                throw new NegocioException("Empréstimo não encontrado.");

            if (emprestimo.Status != StatusEmprestimo.ATIVO)
                throw new NegocioException("Não é possível devolver um empréstimo que não está ativo.");

            emprestimo.DataRealDevolucao = DateTime.Now;

            if (emprestimo.DataRealDevolucao > emprestimo.DataPrevistaDevolucao)
            {
                emprestimo.Status = StatusEmprestimo.ATRASADO;
                int diasAtraso = (emprestimo.DataRealDevolucao.Value - emprestimo.DataPrevistaDevolucao).Days;

                var multa = new Multa
                {
                    EmprestimoId = emprestimo.Id,
                    Valor = diasAtraso * 1.00m,
                    Status = StatusMulta.PENDENTE
                };
                _context.Multas.Add(multa);
            }
            else
            {
                emprestimo.Status = StatusEmprestimo.FINALIZADO;
            }

            _livroService.AtualizarStatus(emprestimo.ISBN, StatusLivro.DISPONIVEL);
            _context.SaveChanges();
        }
    }
}
