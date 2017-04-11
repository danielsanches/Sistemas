namespace MVC.Areas.Gerencial.Models.Compras
{
    using System;

    public class ComprasItensViewModel
    {
        public int ProdutoId { get; set; }

        public string ProdutoDescricao { get; set; }

        public int QtdProduto { get; set; }

        public string ValorUn { get; set; }

        public string ValorTotal { get; set; }
    }
}