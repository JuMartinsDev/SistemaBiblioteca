using BibliotecaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly RelatorioService _relatorioService;

        public RelatorioController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("livros-mais-emprestados")]
        public IActionResult LivrosMaisEmprestados()
        {
            return Ok(_relatorioService.LivrosMaisEmprestados());
        }

        [HttpGet("usuarios-com-mais-emprestimos")]
        public IActionResult UsuariosComMaisEmprestimos()
        {
            return Ok(_relatorioService.UsuariosComMaisEmprestimos());
        }

        [HttpGet("emprestimos-atrasados")]
        public IActionResult EmprestimosAtrasados()
        {
            return Ok(_relatorioService.EmprestimosAtrasados());
        }
    }
}
