using BibliotecaApp.Models;
using BibliotecaApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            _usuarioService.CadastrarUsuario(usuario);
            return Ok("Usuário cadastrado com sucesso!");
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            return Ok(_usuarioService.ListarUsuarios());
        }
    }
}
