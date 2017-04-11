namespace MVC.Areas.Gerencial.Models.Vendas
{
    using Clientes;
    using Produtos;
    using System;
    using System.Collections.Generic;

    public class VendasViewModel
    {
        public VendasViewModel()
        {
            ListaVendas = new List<VendasItensViewModel>();
            ListaClientes = new List<ClienteItemViewModel>();
            ListaProdutos = new List<ProdutosItensViewModel>();
        }

        public string ImageUrl { get; set; }

        public string NumeroPedido { get { return Id.ToString().PadLeft(4, '0'); } }

        public int Id { get; set; }
        public string DataVenda { get; set; }
        public string SubTotal { get; set; }
        public string Status { get; set; }

        public string ClienteDescricao { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }

        public string CodigoProduto { get; set; }
        public int Quantidade { get; set; }

        public List<VendasItensViewModel> ListaVendas { get; set; }
        public IEnumerable<ClienteItemViewModel> ListaClientes { get; set; }
        public IEnumerable<ProdutosItensViewModel> ListaProdutos { get; set; }
        public string CpfCnpjCLiente { get; internal set; }
        public string TelefoneCliente { get; internal set; }
    }
}