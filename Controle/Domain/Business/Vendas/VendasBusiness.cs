namespace Domain.Business.Vendas
{
    using System;
    using System.Linq;
    using Domain.Model;

    public class VendasBusiness
    {
        public void ValidarLancamento(Vendas venda)
        {
            if (venda.ClienteId <= 0)
                throw new InvalidOperationException("Não é possível lançar uma venda sem cliente.");
            if (!venda.ItensVenda.Any())
                throw new InvalidOperationException("Não é possível lançar uma venda sem itens.");
            if (venda.Status.Equals("Inativo"))
                throw new InvalidOperationException("Não é possível lançar uma venda com status inativo");
            if (venda.ValorVenda <= 0)
                throw new InvalidOperationException("Não é possível lançar uma venda sem valor.");
            if (venda.ItensVenda.Any(x => x.Quantidade <= 0))
                throw new InvalidOperationException("Não é possível lançar uma venda com item sem quantidade");
            if (venda.ItensVenda.Any(x => x.ValorItem <= 0))
                throw new InvalidOperationException("Não é possível lançar uma venda com item sem valor");
        }
    }
}
