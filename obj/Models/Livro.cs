namespace Biblioteca.Models
{
    public enum Categoria
    {
        FICCAO,
        TECNICO,
        DIDATICO
    }

    public enum StatusLivro
    {
        DISPONIVEL,
        EMPRESTADO,
        RESERVADO
    }

    public class Livro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public Categoria Categoria { get; set; }
        public StatusLivro Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
