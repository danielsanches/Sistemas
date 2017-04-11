using System;

namespace MVC.Areas.Gerencial.Models.Produtos
{
    public class ProdutosItensViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UidadeArmazenamento { get; set; }
        public string ValorVenda { get; set; }
        public string ValorAtacado { get; set; }
        public string PercentualVenda { get; set; }
        public decimal Quantidade { get; set; }
        public string Status { get; set; }
        public string CodigoBarra { get; set; }

        public string ValorTotal
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ValorVenda))
                    return (Quantidade * Convert.ToDecimal(ValorVenda)).ToString("N2");

                return "0,00";
            }
        }
    }
}