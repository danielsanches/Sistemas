namespace Domain.Business.Compras
{
    using Model;
    using System;
    using System.Linq;

    public class ComprasBusiness
    {
        public void ValidarCompra(Compra compra)
        {
            if (!compra.ItensCompra.Any())
                throw new InvalidOperationException("Não é possível cadastrar uma compra sem itens.");

            if (compra.FornecedorId <= 0)
                throw new InvalidOperationException("Não é possível cadastrar uma compra sem fornecedor.");

            if (!compra.DataCompra.HasValue)
                throw new InvalidOperationException("Não é possível cadastrar uma compra sem data da compra.");
        }
    }
}
