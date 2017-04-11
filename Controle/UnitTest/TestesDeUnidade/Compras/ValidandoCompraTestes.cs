namespace UnitTest.TestesDeUnidade.Compras
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Compras;
    using Domain.Model;
    using System.Collections.Generic;
    using System.Linq;
    [TestClass]
    public class ValidandoCompraTestes
    {
        public ComprasBusiness _comprasBusiness;
        public ItensCompraBusiness _itensCompraBusiness;
        public Compra _compra;


        [TestInitialize]
        public void Initializer()
        {
            _itensCompraBusiness = new ItensCompraBusiness();
            _comprasBusiness = new ComprasBusiness();

            var itens = new List<ItensCompra> {
                new ItensCompra { ProdutoId = 1, Quantidade = 3, ValorItem = 76 },
                new ItensCompra { ProdutoId = 2, Quantidade = 5, ValorItem = 87 },
                new ItensCompra { ProdutoId = 3, Quantidade =2, ValorItem = 45 }
            };

            _compra = new Compra
            {
                DataCompra = DateTime.Now,
                FornecedorId = 2,
                ItensCompra = itens,
                ValorCompra = itens.Sum(x => x.ValorTotalItem)
            };

        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoConcluirCompraSemItensTeste()
        {
            _compra.ItensCompra = new List<ItensCompra>();
            _comprasBusiness.ValidarCompra(_compra);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoConcluirCompraComItensComQuantidadeInvalidaTeste()
        {
            _itensCompraBusiness.ValidarItemCompra(new ItensCompra { ProdutoId = 1, Quantidade = 0, ValorItem = 76 });
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoConcluirCompraComItensComValorInvalidoTeste()
        {
            _itensCompraBusiness.ValidarItemCompra(new ItensCompra { ProdutoId = 1, Quantidade = 3 });
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoConcluirCompraSemFornecedorTeste()
        {
            _compra.FornecedorId = 0;
            _comprasBusiness.ValidarCompra(_compra);
        }
    }
}
