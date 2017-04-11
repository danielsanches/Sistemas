namespace MVC.Areas.Gerencial.Models.Estoque
{
    using System.Collections.Generic;

    public class EstoqueViewModel
    {
        public EstoqueViewModel()
        {
            ListaEstoque = new List<EstoqueItemViewModel>();
        }

        public long Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public string Descricao { get; set; }

        public string CodigoBarra { get; internal set; }
        public List<EstoqueItemViewModel> ListaEstoque { get; set; }       
    }
}