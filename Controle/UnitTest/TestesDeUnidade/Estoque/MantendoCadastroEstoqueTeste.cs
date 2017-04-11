namespace UnitTest.TestesDeUnidade.Estoque
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Estoque;
    using Domain.Model;

    [TestClass]
    public class MantendoCadastroEstoqueTeste
    {
        private EstoqueBusiness _estoqueBusiness;

        [TestInitialize]  
        public void Initializer()
        {
            _estoqueBusiness = new EstoqueBusiness();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastraEstoqueSemProdutoTeste()
        {
            var estoque = new Estoque { ProdutoId = 0, Quantidade = 563 };
            _estoqueBusiness.ValidarLancamento(estoque);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastraEstoqueComQuantidadeMenoqueQueZeroTeste()
        {
            var estoque = new Estoque { ProdutoId = 5, Quantidade = -1 };
            _estoqueBusiness.ValidarLancamento(estoque);
        }
    }
}
