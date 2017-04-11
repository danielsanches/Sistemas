using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Gerencial.Models.Clientes
{
    public class ClienteItemViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string FoneFixo { get; set; }
        public string FoneMovel1 { get; set; }
        public string FoneMovel2 { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}