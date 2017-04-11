namespace Application.Services.Produto
{
    using Domain.Model;
    using System.Collections.Generic;

    public class ResponseProduto
    {
        public ResponseProduto()
        {
            ListaProdutos = new List<Produtos>();
        }

        public string Mensagem { get; set; }

        public bool Sucesso { get; set; }

        public Produtos Produto { get; set; }

        public int TotalAtivos { get; internal set; }
        public int TotalInativos { get; internal set; }

        public List<Produtos> ListaProdutos { get; set; }
    }
}