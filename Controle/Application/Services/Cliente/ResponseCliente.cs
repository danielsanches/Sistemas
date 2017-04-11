namespace Application.Services.Cliente
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseCliente
    {
        public ResponseCliente()
        {
            ListaCliente = new List<Cliente>();
        }

        public bool Sucesso { get; internal set; }

        public string Mensagem { get; internal set; }

        public Cliente Cliente { get; set; }

        public List<Cliente> ListaCliente { get; set; }
        public int QtdAtivo { get; internal set; }
        public int QtdInativo { get; internal set; }
    }
}
