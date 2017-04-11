namespace Application.Services.Vendas
{
    using Domain.Model;
    using System;
    using System.Collections.Generic;

    public class RequestVendas
    {
        public RequestVendas()
        {
            ItensVenda = new List<ItensVenda>();
        }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int ClienteId { get; set; }
        public string ClienteDescricao { get; set; }

        public List<ItensVenda> ItensVenda { get; set; }
        
    }
}