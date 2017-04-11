namespace MVC.Areas.Gerencial.Models.Compras
{
    using Fornecedor;
    using Produtos;
    using System;
    using System.Collections.Generic;

    public class ComprasViewModel
    {
        public ComprasViewModel()
        {
            ListaProdutosCompra = new List<ComprasItensViewModel>();
            ListaProdutos = new List<ProdutosItensViewModel>();
            ListaFornecedores = new List<FornecedorItemViewModel>();
            ListaCompras = new List<ListaComprasViewModel>();
        }

        public int Id { get; set; }
        public string Produto { get; set; }
        public int? FornecedorId { get; set; }
        public DateTime? DataCompra { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public decimal? ValorCompra { get; set; }
        public string Status { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public int QtdProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public string AcaoBotao { get; set; }

        public List<ComprasItensViewModel> ListaProdutosCompra { get; set; }
        public List<ListaComprasViewModel> ListaCompras { get; set; }
        public List<ProdutosItensViewModel> ListaProdutos { get; set; }
        public List<FornecedorItemViewModel> ListaFornecedores { get; set; }
    }
}