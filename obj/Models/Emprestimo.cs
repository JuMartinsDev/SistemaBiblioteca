namespace Biblioteca.Models
{
    public enum StatusEmprestimo
    {
        ATIVO,
        FINALIZADO,
        ATRASADO
    }

    public class Emprestimo
    {
        public int Id { get; set; } 
        public string ISBNLivro { get; set; } 
        public int IdUsuario { get; set; } 
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataRealDevolucao { get; set; }
        public StatusEmprestimo Status { get; set; }
    }
}
