namespace MVC.Areas.Gerencial.Models.SubGrupoProduto
{
    using MVC.Areas.Gerencial.Models.GrupoProduto;
    using System.Collections.Generic;

    public class SubGrupoProdutoViewModel
    {
        public SubGrupoProdutoViewModel()
        {
            ListaGrupoProdutos = new List<GrupoProdutoViewModel>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string GrupoId { get; set; }
        public string DescricaoGrupo { get; internal set; }

        public bool Status { get; set; }

        public IEnumerable<GrupoProdutoViewModel> ListaGrupoProdutos { get; set; }
    }
}