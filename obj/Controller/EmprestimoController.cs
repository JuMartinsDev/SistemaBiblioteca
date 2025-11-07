using BibliotecaApp.Models;
using BibliotecaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;

        public EmprestimoController(EmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
        public IActionResult RegistrarEmprestimo([FromBody] Emprestimo emprestimo)
        {
            try
            {
                _emprestimoService.RegistrarEmprestimo(emprestimo);
                return Ok("Empréstimo registrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/devolucao")]
        public IActionResult RegistrarDevolucao(int id)
        {
            try
            {
                _emprestimoService.RegistrarDevolucao(id);
                return Ok("Devolução registrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("atrasados")]
        public IActionResult ListarEmprestimosAtrasados()
        {
            return Ok(_emprestimoService.ListarEmprestimosAtrasados());
        }
    }
}
