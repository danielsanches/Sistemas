namespace MVC.Areas.Gerencial.Models.Fornecedor
{
    public class FornecedorItemViewModel
    {
        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        public string Status { get; set; }

        public string FoneFixo { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public System.DateTime DataCadastro { get; set; }
    }
}