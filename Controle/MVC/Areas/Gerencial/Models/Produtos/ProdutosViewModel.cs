namespace MVC.Areas.Gerencial.Models.Produtos
{
    using GrupoProduto;
    using SubGrupoProduto;
    using System.Collections.Generic;

    public class ProdutosViewModel
    {
        public ProdutosViewModel()
        {
            ListaProdutos = new List<ProdutosItensViewModel>();
            ListaSubGrupo = new List<SubGrupoProdutoViewModel>();
            ListaGrupoProduto = new List<GrupoProdutoViewModel>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UidadeArmazenamento { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorAtacado { get; set; }
        public decimal PercentualVenda { get; set; }
        public decimal ValorVenda { get; set; }
        public decimal Quantidade { get; set; }
        public bool Status { get; set; }
        public string CodigoBarra { get; set; }

        public string SubGrupoProduto { get; set; }

        public string GrupoProduto { get; set; }

        public IEnumerable<SubGrupoProdutoViewModel> ListaSubGrupo { get; set; }
        public IEnumerable<GrupoProdutoViewModel> ListaGrupoProduto { get; set; }
        public IEnumerable<ProdutosItensViewModel> ListaProdutos { get; set; }
    }
}