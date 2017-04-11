namespace Domain.Business.Produto
{
    using System.Collections.Generic;
    using Domain.Model;

    public class RequestProdutoBusiness
    {
        public RequestProdutoBusiness()
        {
            ListaProdutos = new List<Produtos>();
        }

        public bool Cadastrar { get; set; }

        public Produtos Produto { get; set; }
        public List<Produtos> ListaProdutos { get; set; }
    }
}