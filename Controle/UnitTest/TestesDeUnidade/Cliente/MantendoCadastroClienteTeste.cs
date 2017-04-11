namespace UnitTest.TestesDeUnidade.Cliente
{
    using System;
    using Domain.Model;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Cliente;
    using Domain.Enum;
    using Moq;
    using Domain.Interfaces;

    [TestClass]
    public class MantendoCadastroClienteTeste
    {
        private ClienteBusiness _clienteBusiness;
        private RequestClienteBusiness _requestClienteBusiness;
        private Mock<IRepository<Cliente>> _moqClienteRepository;

        [TestInitialize]
        public void Initializer()
        {
            _moqClienteRepository = new Mock<IRepository<Cliente>>();
            _clienteBusiness = new ClienteBusiness(new Domain.Business.ValidacoesCadastro(), _moqClienteRepository.Object);
            CriarCenarioQueAtendeAoContexto();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarClienteComDescricaoInvalidaTeste()
        {
            _requestClienteBusiness.Cliente = new Cliente { Nome = "", Status = EnumSituacao.Ativo.ToString() };
            _clienteBusiness.ValidarCadastro(_requestClienteBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarClienteComStatusInativoTeste()
        {
            _requestClienteBusiness.Cliente = new Cliente { Nome = "", Status = EnumSituacao.Inativo.ToString() };
            _clienteBusiness.ValidarCadastro(_requestClienteBusiness);
        }

        [Ignore]
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarClienteComNomeRepetidoTeste()
        {
            _requestClienteBusiness.Cliente = new Cliente { Id= 1, Nome = "Maria", Status = EnumSituacao.Ativo.ToString() };
            _clienteBusiness.ValidarCadastro(_requestClienteBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarClienteComEmailInvalidoTeste()
        {
            _requestClienteBusiness.Cliente = new Cliente { Nome = "Maria", Status = EnumSituacao.Ativo.ToString(), Email = "daniel@" };
            _clienteBusiness.ValidarCadastro(_requestClienteBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarClienteComCpfInvalidoTeste()
        {
            _requestClienteBusiness.Cliente = new Cliente { Nome = "Maria", Status = EnumSituacao.Ativo.ToString(), Cpf = "02526325596" };
            _clienteBusiness.ValidarCadastro(_requestClienteBusiness);
        }

        private void CriarCenarioQueAtendeAoContexto()
        {
            var lista = new List<Cliente>
            {
                new Cliente {Id=2, Nome ="Maria", Status = EnumSituacao.Ativo.ToString() },
                new Cliente {Id=3, Nome ="Elena", Status = EnumSituacao.Ativo.ToString() },
                new Cliente {Id=4, Nome ="Tereza", Status = EnumSituacao.Ativo.ToString() },
            };

            _requestClienteBusiness = new RequestClienteBusiness { };
        }
    }
}
