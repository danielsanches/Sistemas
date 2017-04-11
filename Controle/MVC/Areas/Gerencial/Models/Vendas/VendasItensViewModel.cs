namespace MVC.Areas.Gerencial.Models.Vendas
{
    using System;

    public class VendasItensViewModel
    {
        public int Id { get; set; }
        public string CodigoBarra { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorVenda { get; set; }
        public string Status { get; set; }
        public int ClienteId { get; set; }

        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorItem { get; set; }
        public string ValorItemFormatado
        {
            get { return ValorItem.ToString("N2"); }
        }

        public string TotalItemFormatado { get { return TotalItem.ToString("N2"); } }

        public decimal TotalItem { get; set; }
    }
}