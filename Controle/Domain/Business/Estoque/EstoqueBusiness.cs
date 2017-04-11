namespace Domain.Business.Estoque
{
    using System;
    using Model;

    public class EstoqueBusiness
    {
        public void ValidarLancamento(Estoque estoque)
        {
            if (estoque.ProdutoId <= 0)
                throw new InvalidOperationException("Não é possível cadastrar estoque sem produto.");

            if (estoque.Quantidade < 0)
                throw new InvalidOperationException("Não é possível cadastrar produto com estoque menor que zero.");
        }
    }
}
