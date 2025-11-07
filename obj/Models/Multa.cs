namespace Biblioteca.Models
{
    public enum StatusMulta
    {
        PENDENTE,
        PAGA
    }

    public class Multa
    {
        public int IdEmprestimo { get; set; }
        public decimal Valor { get; set; }
        public StatusMulta Status { get; set; }
    }
}
