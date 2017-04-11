namespace UnitTest.TestesDeUnidade.Fornecedor
{
    using Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Fornecedor;
    using System.Collections.Generic;
    using System;
    using Domain.Enum;

    [TestClass]
    public class MantendoCadastroFornecedorTeste
    {
        private FornecedorBusiness _fornecedorBusiness;
        private RequestFornecedorBusiness _requestFornecedoresBusiness;

        [TestInitialize]
        public void Initializer()
        {
            _fornecedorBusiness = new FornecedorBusiness();
            FornecedorQueAtendeAoCenario();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarFornecedorSemDescricaoTeste()
        {
            _requestFornecedoresBusiness.Fornecedor.NomeFantasia = "";
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarFornecedorSemTelefoneTeste()
        {
            _requestFornecedoresBusiness.Fornecedor.FoneFixo = "";
            _requestFornecedoresBusiness.Fornecedor.Celular = "";
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarFornecedorComStatusInativoTeste()
        {
            _requestFornecedoresBusiness.Fornecedor.Status = EnumSituacao.Inativo.ToString();
            _requestFornecedoresBusiness.Cadastrar = true;
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarFornecedorComEmailInvalidoTeste()
        {
            _requestFornecedoresBusiness.Fornecedor.Email = "forn@com";
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarFornecedorComDescricaoRepetidaTeste()
        {
            _requestFornecedoresBusiness.Cadastrar = true;
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoAlterarFornecedorComDescricaoRepetidaTeste()
        {
            _requestFornecedoresBusiness.Cadastrar = false;
            _requestFornecedoresBusiness.ListaFornecedor[1].NomeFantasia = "Descricao";
            _fornecedorBusiness.ValidarCadastro(_requestFornecedoresBusiness);
        }

        private void FornecedorQueAtendeAoCenario()
        {
            var fornecedor = new Fornecedor { Id = 3, NomeFantasia = "Descricao", Celular = "89886954", FoneFixo = "6733659654", Email = "fornecedor@forn.com" };
            var lista = new List<Fornecedor>
            {
                new Fornecedor {Id=1, NomeFantasia = "Descricao1", Celular = "89886954", FoneFixo = "6733659654", Email = "fornecedor@forn.com" },
                new Fornecedor {Id=2, NomeFantasia = "Descricao2", Celular = "89886954", FoneFixo = "6733659654", Email = "fornecedor@forn.com" },
                new Fornecedor {Id=3, NomeFantasia = "Descricao", Celular = "89886954", FoneFixo = "6733659654", Email = "fornecedor@forn.com" }
            };

            _requestFornecedoresBusiness = new RequestFornecedorBusiness
            {
                Fornecedor = fornecedor,
                ListaFornecedor = lista
            };
        }
    }
}
