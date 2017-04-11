namespace UnitTest.TestesDeUnidade.SubGrupoProduto
{
    using System;
    using System.Collections.Generic;
    using Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.SubGrupoProduto;

    [TestClass]
    public class MantendoCadastroSubGrupoTeste
    {
        private SubGrupoProdutoBusiness _subGrupoProdutoBusiness;
        private RequestSubGrupoBusiness _requestSubGrupoBusiness;

        [TestInitialize]
        public void Initializer()
        {
            _subGrupoProdutoBusiness = new SubGrupoProdutoBusiness();
            _requestSubGrupoBusiness = new RequestSubGrupoBusiness();
            CriarObjetoQueAtendeAoCenarioTeste();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarSubGrupoProdutoComDescricaoInvalidaTeste()
        {
            _requestSubGrupoBusiness.SubGrupoProduto.Descricao = "";
            _subGrupoProdutoBusiness.ValidarCadastro(_requestSubGrupoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarSubgrupoProdutoSemGrupoProdutoTeste()
        {
            _requestSubGrupoBusiness.SubGrupoProduto.GrupoProdutoId = null;
            _subGrupoProdutoBusiness.ValidarCadastro(_requestSubGrupoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarSubGrupoProdutoComDescricaoRepetidaTeste()
        {
            _requestSubGrupoBusiness.Cadastrar = true;
            _subGrupoProdutoBusiness.ValidarCadastro(_requestSubGrupoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoAlterarSubGrupoProdutoComDescricaoRepetidaTeste()
        {
            _requestSubGrupoBusiness.Cadastrar = false;
            _subGrupoProdutoBusiness.ValidarCadastro(_requestSubGrupoBusiness);
        }

        private void CriarObjetoQueAtendeAoCenarioTeste()
        {
            var subgrupo = new SubGrupoProduto { Id = 4, Descricao = "Descricao", GrupoProdutoId = 1 };

            var lista = new List<SubGrupoProduto>
            {
                new SubGrupoProduto { Id = 1, Descricao = "Descricao1", GrupoProdutoId = 1 },
                new SubGrupoProduto { Id = 2, Descricao = "Descricao2", GrupoProdutoId = 2 },
                new SubGrupoProduto { Id = 3, Descricao = "Descricao", GrupoProdutoId = 3 }
            };

            _requestSubGrupoBusiness = new RequestSubGrupoBusiness
            {
                Cadastrar = false,
                SubGrupoProduto = subgrupo,
                ListaSubGrupoProduto = lista
            };
        }
    }
}
