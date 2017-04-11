namespace UnitTest.TesteDeIntegracao.MantendoEstoqueTeste
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Domain.Model;
    using Domain.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    
    [TestClass]
    public class CadastradoCompraComEstoqueTeste
    {
        private Mock<IRepository<Compra>> _compraRepository;
        private Mock<IRepository<Estoque>> _estoqueRepository;

        [TestInitialize]
        public void Initializer()
        {
            _compraRepository = new Mock<IRepository<Compra>>();
            _estoqueRepository = new Mock<IRepository<Estoque>>();
            _compraRepository.Setup(x => x.Cadastrar(It.IsAny<Compra>()));
        }

        [TestMethod]
        public void ConsigoCadastrarCompraComLancamentoEmEstoqueTeste()
        {
            var itens = new List<ItensCompra> {
                new ItensCompra { ProdutoId = 1, Quantidade = 3, ValorItem = 76 },
                new ItensCompra { ProdutoId = 2, Quantidade = 5, ValorItem = 87 },
                new ItensCompra { ProdutoId = 3, Quantidade =2, ValorItem = 45 }
            };

            var compra = new Compra
            {
                DataCompra = DateTime.Now,
                FornecedorId = 2,
                ItensCompra = itens,
                ValorCompra = itens.Sum(x => x.ValorTotalItem)
            };

            _compraRepository.Object.Cadastrar(compra);

            foreach (var item in compra.ItensCompra)
            {
                var estoque = new Estoque
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade
                };

                _estoqueRepository.Object.Cadastrar(estoque);
            }

            _compraRepository.Object.SaveChanges();
            _estoqueRepository.Object.SaveChanges();

            _compraRepository.Verify(p => p.Cadastrar(It.IsAny<Compra>()), Times.Once());
        }
    }
}
