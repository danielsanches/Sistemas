namespace UnitTest.TestesDeUnidade.GrupoProduto
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.GrupoProduto;
    using Domain.Model;
    using Domain.Enum;
    using System.Collections.Generic;

    [TestClass]
    public class MantendoCadastroGrupoProdutoTeste
    {
        private GrupoProdutoBusiness _grupoProdutoBusiness;
        private RequestGrupoProdutoBusiness _requestGrupoProdutoBusiness;

        [TestInitialize]
        public void Initializer()
        {
            _grupoProdutoBusiness = new GrupoProdutoBusiness();
            CriandoObjetoQueAtendeAoCenario();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarGrupoProdutoSemDescricaoTeste()
        {
            _requestGrupoProdutoBusiness.GrupoProduto.Descricao = "";
            _grupoProdutoBusiness.ValidarCadastro(_requestGrupoProdutoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarGrupoProdutoComStatusInativoTeste()
        {
            _requestGrupoProdutoBusiness.GrupoProduto.Status = EnumSituacao.Inativo.ToString();
            _grupoProdutoBusiness.ValidarCadastro(_requestGrupoProdutoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarGrupoProdutoComDescricaoRepetidaTeste()
        {
            _requestGrupoProdutoBusiness.Cadastrar = true;
            _grupoProdutoBusiness.ValidarCadastro(_requestGrupoProdutoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoAlterarGrupoProdutoComDescricaoRepetidaTeste()
        {
            _requestGrupoProdutoBusiness.Cadastrar = false;
            _grupoProdutoBusiness.ValidarCadastro(_requestGrupoProdutoBusiness);
        }

        private void CriandoObjetoQueAtendeAoCenario()
        {
            var grupoProduto = new GrupoProduto { Id = 1, Descricao = "Descricao", Status = EnumSituacao.Ativo.ToString() };

            var lista = new List<GrupoProduto>
            {
                new GrupoProduto { Id = 1, Descricao = "Descricao1", Status = EnumSituacao.Ativo.ToString() },
                new GrupoProduto { Id = 2, Descricao = "Descricao2", Status = EnumSituacao.Ativo.ToString() },
                new GrupoProduto { Id = 3, Descricao = "Descricao", Status = EnumSituacao.Ativo.ToString() }
            };

            _requestGrupoProdutoBusiness = new RequestGrupoProdutoBusiness
            {
                GrupoProduto = grupoProduto,
                ListaGrupos = lista
            };
        }
    }
}
