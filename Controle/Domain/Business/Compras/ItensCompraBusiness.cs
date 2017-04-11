namespace Domain.Business.Compras
{
    using Domain.Model;
    using System;

    public class ItensCompraBusiness
    {
        public void ValidarItemCompra(ItensCompra item)
        {
            if (item.Quantidade <= 0)
                throw new InvalidOperationException("Favor informar a quantidade dos itens.");

            if (!item.ValorItem.HasValue)
                throw new InvalidOperationException("Favor informar o valor do item.");
        }
    }
}
