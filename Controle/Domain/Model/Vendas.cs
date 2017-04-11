namespace Domain.Model
{
    using System;
    using System.Collections.Generic;

    public class Vendas
    {
        public Vendas()
        {
            ItensVenda = new List<ItensVenda>();
        }

        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorVenda { get; set; }
        public string Status { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ItensVenda> ItensVenda { get; set; }
    }
}
