namespace Application.Services.SubGrupoProduto
{
    using Domain.Business.SubGrupoProduto;
    using System;
    using System.Linq;
    using Domain.Model;
    using Domain.Interfaces;
    using Domain.Enum;
    using System.Collections.Generic;
    public class SubGrupoProdutoAppService
    {
        private IRepository<SubGrupoProduto> _subGrupoRepository;
        private SubGrupoProdutoBusiness _subGrupoProdutoBusiness;

        public SubGrupoProdutoAppService(IRepository<SubGrupoProduto> subGrupoRepository, SubGrupoProdutoBusiness subGrupoProdutoBusiness)
        {
            _subGrupoProdutoBusiness = subGrupoProdutoBusiness;
            _subGrupoRepository = subGrupoRepository;
        }

        public ResponseSubGrupoProduto Cadastrar(RequestSubGrupoProduto request)
        {
            try
            {
                var subGrupo = new SubGrupoProduto
                {
                    Descricao = request.Descricao,
                    GrupoProdutoId = request.GrupoProdutoId
                };

                ValidarCadastro(subGrupo, true);
                subGrupo.Descricao = subGrupo.Descricao.ToUpper();
                _subGrupoRepository.Cadastrar(subGrupo);
                _subGrupoRepository.SaveChanges();

                return new ResponseSubGrupoProduto { Sucesso = true, Mensagem = "SubGrupo de produtos cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseSubGrupoProduto Alterar(RequestSubGrupoProduto request)
        {
            try
            {
                var subGrupo = _subGrupoRepository.Obter(request.Id);
                if (subGrupo == null)
                    throw new Exception("Subgrupo do produto não encontrado.");

                subGrupo.Descricao = request.Descricao;
                subGrupo.GrupoProdutoId = request.GrupoProdutoId;
                subGrupo.Status = request.Status;

                ValidarCadastro(subGrupo);
                subGrupo.Descricao = subGrupo.Descricao.ToUpper();
                _subGrupoRepository.Alterar(subGrupo);
                _subGrupoRepository.SaveChanges();

                return new ResponseSubGrupoProduto { Sucesso = true, Mensagem = "SubGrupo de produtos alterado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseSubGrupoProduto ObterTotaisProdutos(RequestSubGrupoProduto produto)
        {
            try
            {
                var lista = FiltarLista(produto);
                var totaisAtivos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Ativo.ToString().ToUpper()));
                var totaisInativos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Inativo.ToString().ToUpper()));

                return new ResponseSubGrupoProduto
                {
                    Sucesso = true,
                    TotalAtivos = totaisAtivos,
                    TotalInativos = totaisInativos
                };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseSubGrupoProduto Obter(int id)
        {
            try
            {
                var subGrupo = _subGrupoRepository.Obter(id);
                if (subGrupo == null)
                    throw new Exception("Subgrupo do produto não encontrado.");


                return new ResponseSubGrupoProduto { Sucesso = true, SubGrupoProduto = subGrupo };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = true, Mensagem = ex.Message };
            }
        }

        public ResponseSubGrupoProduto ObterLista(RequestSubGrupoProduto request)
        {
            try
            {
                var lista = FiltarLista(request).Where(x => x.Status.ToUpper().Equals(request.Status.ToUpper())).ToList();

                return new ResponseSubGrupoProduto { Sucesso = true, ListaSubGrupoProduto = lista };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseSubGrupoProduto ObterListaAtivos()
        {
            try
            {
                var lista = _subGrupoRepository.ObterTodos().ToList();

                return new ResponseSubGrupoProduto { Sucesso = true, ListaSubGrupoProduto = lista };
            }
            catch (Exception ex)
            {
                return new ResponseSubGrupoProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        private List<SubGrupoProduto> FiltarLista(RequestSubGrupoProduto request)
        {
            var temDescricao = !string.IsNullOrEmpty(request.Descricao);
            return _subGrupoRepository.ObterPor(x => temDescricao
                                                         ? x.Descricao == request.Descricao
                                                         : x.Descricao != null
                                                      && request.GrupoProdutoId.HasValue
                                                         ? x.GrupoProdutoId == request.GrupoProdutoId
                                                         : x.GrupoProdutoId.HasValue
                                                         ).ToList();
        }

        private void ValidarCadastro(SubGrupoProduto subGrupo, bool cadastrar = false)
        {
            var lista = _subGrupoRepository.ObterTodos().ToList();
            var request = new RequestSubGrupoBusiness
            {
                Cadastrar = cadastrar,
                ListaSubGrupoProduto = lista,
                SubGrupoProduto = subGrupo
            };

            _subGrupoProdutoBusiness.ValidarCadastro(request);
        }
    }
}
