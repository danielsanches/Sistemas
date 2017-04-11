namespace MVC.Areas.Gerencial.Models.GrupoProduto
{
    using System.Collections.Generic;

    public class GrupoProdutoViewModel
    {
        public GrupoProdutoViewModel()
        {
            ListaGrupoProduto = new List<GrupoProdutoItemViewModel>();
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public bool Status { get; set; }

        public List<GrupoProdutoItemViewModel> ListaGrupoProduto { get; set; }
    }
}