namespace Application.Services.Vendas
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseVendas
    {
        public ResponseVendas()
        {
            ListaVendas = new List<Vendas>();
        }

        public string Mensagem { get; set; }

        public bool Sucesso { get; set; }

        public Vendas Venda { get; set; }

        public List<Vendas> ListaVendas { get; set; }
        public int VendaId { get; internal set; }
    }
}
