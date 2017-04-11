using System.Collections.Generic;

namespace MVC.Areas.Gerencial.Models.Fornecedor
{
    public class FornecedorViewModel
    {
        public FornecedorViewModel()
        {
            ListaFornecedor = new List<FornecedorItemViewModel>();
        }

        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        public bool Status { get; set; }

        public string FoneFixo { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public System.DateTime DataCadastro { get; set; }

        public List<FornecedorItemViewModel> ListaFornecedor { get; set; }
    }
}