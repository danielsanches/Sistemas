namespace Application.Services.GrupoProduto
{
    using Domain.Business.GrupoProduto;
    using System;
    using System.Linq;
    using Domain.Model;
    using Domain.Interfaces;
    using Domain.Enum;
    using System.Collections.Generic;
    using Infra.Reposiotry;

    public class GrupoProdutoAppService
    {
        private GrupoProdutoBusiness _grupoProdutoBusiness;
        private IRepository<GrupoProduto> _grupoProdutoRepository;

        public GrupoProdutoAppService()
        {
            _grupoProdutoRepository = new GrupoProdutoRepository();
            _grupoProdutoBusiness = new GrupoProdutoBusiness();
        }

        public ResponseGrupoProduto Cadastrar(RequestGrupoProduto request)
        {
            try
            {
                var grupo = new GrupoProduto { Descricao = request.Descricao };

                ValidarCadastro(grupo, true);
                grupo.Descricao = grupo.Descricao.ToUpper();
                _grupoProdutoRepository.Cadastrar(grupo);
                _grupoProdutoRepository.SaveChanges();

                return new ResponseGrupoProduto { Sucesso = true, Mensagem = "Grupo de Produto cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseGrupoProduto Alterar(RequestGrupoProduto request)
        {
            try
            {
                var grupo = _grupoProdutoRepository.Obter(request.Id);
                if (grupo == null)
                    throw new Exception("Grupo de Produtos não encontrado.");

                grupo.Descricao = request.Descricao.Trim().ToUpper();
                grupo.Status = request.Situacao;

                ValidarCadastro(grupo);

                grupo.Descricao = request.Descricao.Trim().ToUpper();

                _grupoProdutoRepository.Alterar(grupo);
                _grupoProdutoRepository.SaveChanges();

                return new ResponseGrupoProduto { Sucesso = true, Mensagem = "Grupo de Produto alterado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseGrupoProduto ObterTotaisProdutos(RequestGrupoProduto produto)
        {
            try
            {
                var lista = FiltarLista(produto);
                var totaisAtivos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Ativo.ToString().ToUpper()));
                var totaisInativos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Inativo.ToString().ToUpper()));

                return new ResponseGrupoProduto
                {
                    Sucesso = true,
                    TotalAtivos = totaisAtivos,
                    TotalInativos = totaisInativos
                };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseGrupoProduto Obter(int id)
        {
            try
            {
                var grupo = _grupoProdutoRepository.Obter(id);
                if (grupo == null)
                    throw new Exception("Grupo de Produtos não encontrado.");

                return new ResponseGrupoProduto { Sucesso = true, GrupoProduto = grupo };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseGrupoProduto ObterLista(RequestGrupoProduto request)
        {
            try
            {
                var lista = FiltarLista(request).Where(x => x.Status.ToUpper().Equals(request.Situacao.ToUpper()) ).ToList();

                return new ResponseGrupoProduto { Sucesso = true, ListaGrupoProduto = lista };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public List<GrupoProduto> FiltarLista(RequestGrupoProduto request)
        {

                var descricaoVazia = string.IsNullOrWhiteSpace(request.Descricao);
                return _grupoProdutoRepository.ObterPor(x => descricaoVazia 
                                                            ? !string.IsNullOrEmpty(x.Descricao) 
                                                            : x.Descricao == request.Descricao).ToList();
        }

        public ResponseGrupoProduto ObterListaAtivos()
        {
            try
            {

                var lista = _grupoProdutoRepository.ObterTodos().ToList();

                return new ResponseGrupoProduto { Sucesso = true, ListaGrupoProduto = lista };
            }
            catch (Exception ex)
            {
                return new ResponseGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        private void ValidarCadastro(GrupoProduto grupo, bool cadastrar = false)
        {
            var lista = _grupoProdutoRepository.ObterTodos().ToList();
            _grupoProdutoBusiness.ValidarCadastro(new RequestGrupoProdutoBusiness
            {
                Cadastrar = cadastrar,
                GrupoProduto = grupo,
                ListaGrupos = lista
            });
        }
    }
}
