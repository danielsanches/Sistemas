namespace Application.Services.Compras
{
    using Domain.Business.Compras;
    using Domain.Model;
    using System;

    public class ItemCompraAppService
    {
        public ItensCompraBusiness _itensCompraBusiness;

        public ItemCompraAppService(ItensCompraBusiness itensCompraBusiness)
        {
            _itensCompraBusiness = itensCompraBusiness;
        }

        public ResponseCompra ValidarItem(ItensCompra item)
        {
            try
            {
                _itensCompraBusiness.ValidarItemCompra(item);

                return new ResponseCompra { Sucesso = true };
            }
            catch (Exception ex)
            {
                return new ResponseCompra { Sucesso = false, Mensagem = ex.Message };
            }
        }
    }
}
