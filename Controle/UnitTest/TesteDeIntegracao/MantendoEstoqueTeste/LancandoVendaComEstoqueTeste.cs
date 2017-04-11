using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Interfaces;
using Domain.Model;
using Domain.Business.Vendas;
using Application.Services.Estoque;
using Domain.Business.Estoque;

namespace UnitTest.TesteDeIntegracao.MantendoEstoqueTeste
{
    [TestClass]
    public class LancandoVendaComEstoqueTeste
    {
        private Mock<IRepository<Vendas>> _vendasRepository;
        private Mock<IRepository<Estoque>> _estoqueRepository;
        private VendasBusiness _vendasBusiness;
        private EstoqueAppService _estoqueAppService;

        [TestInitialize]
        public void Inicializa()
        {
            _vendasRepository = new Mock<IRepository<Vendas>>();
            _estoqueRepository = new Mock<IRepository<Estoque>>();
            _vendasBusiness = new VendasBusiness();
            _estoqueAppService = new EstoqueAppService(_estoqueRepository.Object, new EstoqueBusiness());
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
