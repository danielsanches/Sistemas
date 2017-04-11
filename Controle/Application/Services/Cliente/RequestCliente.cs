namespace Application.Services.Cliente
{
    using Domain.Model;

    public class RequestCliente
    {
        public int Id { internal get; set; }
        public string Descricao { get; set; }
        public Cliente Cliente { get; set; }
    }
}