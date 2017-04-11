namespace Application.Services.Fornecedor
{
    using System.Collections.Generic;
    using Domain.Model;

    public class ResponseFornecedor
    {
        public ResponseFornecedor()
        {
            ListaFornecedores = new List<Fornecedor>();
        }

        public string Mesagem { get; internal set; }

        public bool Sucesso { get; internal set; }

        public int QtdAtivos { get; internal set; }

        public int QtdInativos { get; internal set; }

        public Fornecedor Fornecedor { get; internal set; }

        public List<Fornecedor> ListaFornecedores { get; internal set; }

    }
}
