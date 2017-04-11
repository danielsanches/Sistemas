namespace UnitTest.TestesDeUnidade.Produto
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Business.Produto;
    using Domain.Model;
    using Domain.Enum;

    [TestClass]
    public class MantendoCadastroProdutoTeste
    {
        private ProdutoBusiness _produtoBusiness;
        private RequestProdutoBusiness _requestProdutoBusiness;

        [TestInitialize]
        public void Initializer()
        {
            _produtoBusiness = new ProdutoBusiness();
            CriaObjetoQueAtendeAoCenario();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarProdutoComDescricaoInvalidaTeste()
        {
            _requestProdutoBusiness.Produto.Descricao = "";
            _produtoBusiness.ValidarCadastro(_requestProdutoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarProdutoComStatusInativoTeste()
        {
            _requestProdutoBusiness.Produto.Status = EnumSituacao.Inativo.ToString();
            _produtoBusiness.ValidarCadastro(_requestProdutoBusiness);
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoCadastrarProdutoComDescricaoRepetidaTeste()
        {
            _requestProdutoBusiness.Cadastrar = true;
            _produtoBusiness.ValidarCadastro(_requestProdutoBusiness);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void NaoConsigoAlterarProdutoComDescricaoRepetidaTeste()
        {
            _requestProdutoBusiness.Cadastrar = false;
            _produtoBusiness.ValidarCadastro(_requestProdutoBusiness);
        }

        private void CriaObjetoQueAtendeAoCenario()
        {
            var produto = new Produtos { Id = 4, Descricao = "Descricao", CodigoBarra = "2536589666458", Status = EnumSituacao.Ativo.ToString(), SubGrupoProdutoId = 1 };

            var lista = new List<Produtos>
            {
                new Produtos { Id = 1, Descricao = "Descricao1", CodigoBarra = "2536589666458", Status = EnumSituacao.Ativo.ToString(), SubGrupoProdutoId = 1 },
                new Produtos { Id = 2, Descricao = "Descricao2", CodigoBarra = "2536589666458", Status = EnumSituacao.Ativo.ToString(), SubGrupoProdutoId = 2 },
                new Produtos { Id = 3, Descricao = "Descricao", CodigoBarra = "2536589666458", Status = EnumSituacao.Ativo.ToString(), SubGrupoProdutoId = 3 }
            };

            _requestProdutoBusiness = new RequestProdutoBusiness
            {
                Cadastrar = false,
                ListaProdutos = lista,
                Produto = produto
            };
        }
    }
}
