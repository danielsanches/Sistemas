namespace Domain.Model
{
    using Enum;
    using System.Collections.Generic;
    using System;

    public class Produtos
    {
        public Produtos()
        {
            ItensCompra = new List<ItensCompra>();
            ItensVenda = new List<ItensVenda>();
            Status = EnumSituacao.Ativo.ToString();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnidadeArmazenamento { get; set; }
        public decimal ValorAtacado { get; set; }
        public decimal PercentualVenda { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal Quantidade { get; set; }
        public string Status { get; set; }
        public string CodigoBarra { get; set; }

        public int SubGrupoProdutoId { get; set; }
        public int? CompraId { get; set; }
        public int EstoqueId { get; set; }

        public virtual Estoque Estoque { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual SubGrupoProduto SubGrupoProduto { get; set; }
        public virtual ICollection<ItensVenda> ItensVenda { get; set; }
        public virtual ICollection<ItensCompra> ItensCompra { get; set; }

        public void CalcularPorcentagemVenda()
        {
            if (PercentualVenda > 0 && ValorAtacado > 0)
                ValorVenda = Math.Round((ValorAtacado + ((PercentualVenda / 100) * ValorAtacado)),2);
        }

        public string ObterDescricao()
        {
            return Descricao?.ToUpper().Trim();
        }
    }
}
