namespace MVC.Areas.Gerencial.Models.Compras
{
    using System;
    using System.Collections.Generic;

    public class ListaComprasViewModel
    {
        public ListaComprasViewModel()
        {
            ListaItens = new List<ListaComprasItensViewModel>();
        }

        public int Id { get; set; }
        public string Fornecedor { get; set; }
        public string Produto { get; set; }
        public int? FornecedorId { get; set; }
        public string FornecedorDescricao { get; set; }
        public DateTime? DataCompra { get; set; }
        public DateTime DataLancamento { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string ValorCompra { get; set; }
        public string Status { get; set; }

        public string DataCompraFormatada { get; set; }
        public string DataLancamentoFormatada { get; set; }
        public string DataAlteracaoFornatada { get; set; }

        public List<ListaComprasItensViewModel> ListaItens { get; set; }
    }

    public class ListaComprasItensViewModel
    {
        public string Descricao { get; set; }
        public string Quantidade { get; set; }
        public string ValorUnitario { get; set; }
        public string ValorTotal { get; set; }
    }
}