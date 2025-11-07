namespace Biblioteca.Models
{
    public enum TipoUsuario
    {
        ALUNO,
        PROFESSOR,
        FUNCIONARIO
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
