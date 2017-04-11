namespace Domain.Business.Fornecedor
{
    using Domain.Model;
    using System.Collections.Generic;

    public class RequestFornecedorBusiness
    {
        public RequestFornecedorBusiness()
        {
            ListaFornecedor = new List<Fornecedor>();
        }

        public bool Cadastrar { get; set; }

        public Fornecedor Fornecedor { get; set; }

        public List<Fornecedor> ListaFornecedor { get; set; }
    }
}