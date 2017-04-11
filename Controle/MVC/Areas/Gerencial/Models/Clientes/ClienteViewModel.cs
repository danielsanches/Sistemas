namespace MVC.Areas.Gerencial.Models.Clientes
{
    using System;
    using System.Collections.Generic;

    public class ClienteViewModel
    {
        public ClienteViewModel()
        {
            ListaClientes = new List<ClienteItemViewModel>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string FoneFixo { get; set; }
        public string FoneMovel1 { get; set; }
        public string FoneMovel2 { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DataCadastroFormatada { get { return DataCadastro.ToShortDateString(); } }
        public string DataNascimentoFormatada { get { return DataNascimento.HasValue ? DataNascimento.Value.ToShortDateString() : string.Empty; } }

        public List<ClienteItemViewModel> ListaClientes { get; set; }
    }
}