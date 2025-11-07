using BibliotecaApp.Models;
using BibliotecaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivroController(LivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public IActionResult CadastrarLivro([FromBody] Livro livro)
        {
            _livroService.CadastrarLivro(livro);
            return Ok("Livro cadastrado com sucesso!");
        }

        [HttpPut("{isbn}/status")]
        public IActionResult AtualizarStatus(string isbn, [FromQuery] StatusLivro status)
        {
            _livroService.AtualizarStatus(isbn, status);
            return Ok("Status do livro atualizado com sucesso!");
        }

        [HttpGet]
        public IActionResult ListarLivros()
        {
            return Ok(_livroService.ListarLivros());
        }
    }
}
