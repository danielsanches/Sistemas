namespace Domain.Business.Cliente
{
    using Domain.Model;
    using System.Collections.Generic;

    public class RequestClienteBusiness
    {
        public bool Cadastrar { get; set; }
        public Cliente Cliente { get; set; }
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}