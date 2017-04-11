using MVC.Areas.Gerencial.Models.Clientes;
using MVC.Areas.Gerencial.Models.Fornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Areas.Gerencial.Models
{
    public class RelatorioViewModel
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int FornecedorId { get; set; }
        public int ClienteId { get; set; }

        public List<ClienteViewModel> ListaClientes { get; set; } = new List<ClienteViewModel>();
        public List<FornecedorViewModel> ListaFornecedores { get; set; } = new List<FornecedorViewModel>();
    }
}