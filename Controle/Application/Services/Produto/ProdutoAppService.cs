namespace Application.Services.Produto
{
    using Domain.Business.Produto;
    using Domain.Enum;
    using Domain.Interfaces;
    using Domain.Model;
    using Estoque;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Infra.Reposiotry;

    public class ProdutoAppService
    {
        private readonly ProdutoBusiness _produtoBusiness;
        private readonly IRepository<Produtos> _produtosRepository;
        private readonly EstoqueAppService _estoqueAppService;

        public ProdutoAppService()
        {
            _produtoBusiness = new ProdutoBusiness();
            _produtosRepository = new ProdutosRepository();
            _estoqueAppService = new EstoqueAppService();
        }

        public ResponseProduto Cadastrar(RequestProduto request)
        {
            try
            {
                var produto = new Produtos
                {
                    Descricao = request.Descricao,
                    CodigoBarra = request.CodigoBarra,
                    SubGrupoProdutoId = request.SubGrupoProdutoId,
                    UnidadeArmazenamento = request.UidadeArmazenamento,
                    PercentualVenda = request.PercentualVenda
                };

                ValidarCadastro(produto);

                _produtosRepository.Cadastrar(produto);
                _produtosRepository.SaveChanges();

                return new ResponseProduto { Sucesso = true, Mensagem = "Produto cadastrado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto Alterar(RequestProduto request)
        {
            try
            {
                var produto = _produtosRepository.Obter(request.Id);
                if (produto == null)
                    throw new Exception("Produto não econtrado.");

                produto.Descricao = request.Descricao;
                produto.CodigoBarra = request.CodigoBarra;

                if (request.SubGrupoProdutoId > 0)
                    produto.SubGrupoProdutoId = request.SubGrupoProdutoId;

                produto.UnidadeArmazenamento = request.UidadeArmazenamento;
                produto.Status = request.Status;
                produto.PercentualVenda = request.PercentualVenda;
                produto.CalcularPorcentagemVenda();

                ValidarCadastro(produto);

                _produtosRepository.Alterar(produto);
                _produtosRepository.SaveChanges();

                return new ResponseProduto { Sucesso = true, Mensagem = "Produto alterado com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto Obter(int id)
        {
            try
            {
                var produto = _produtosRepository.Obter(id);
                if (produto == null)
                    throw new Exception("Produto não econtrado.");

                return new ResponseProduto { Sucesso = true, Produto = produto };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto ObterProduto(string codigo)
        {
            try
            {
                bool temCodigoBarra = !string.IsNullOrWhiteSpace(codigo);
                var produto = _produtosRepository.ObterPor(x => x.CodigoBarra == codigo).FirstOrDefault();

                if (produto == null)
                    throw new Exception("Produto não econtrado.");

                produto.Estoque = _estoqueAppService.ObterEstoque(produto.Id).Estoque;

                return new ResponseProduto { Sucesso = true, Produto = produto };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto ObterLista(RequestProduto request)
        {
            try
            {
                var lsita = FiltrarLista(request).Where(x => x.Status.ToUpper().Equals(request.Status.ToUpper())).ToList();
                return new ResponseProduto { Sucesso = true, ListaProdutos = lsita };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto ObterListaAtivos()
        {
            try
            {
                var lsita = _produtosRepository.ObterPor(x => x.Status.Equals("Ativo")).ToList();
                return new ResponseProduto { Sucesso = true, ListaProdutos = lsita };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseProduto ObterTotaisProdutos(RequestProduto produto)
        {
            try
            {
                var lista = FiltrarLista(produto);
                var totaisAtivos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Ativo.ToString().ToUpper()));
                var totaisInativos = lista.Count(x => x.Status.ToUpper().Equals(EnumSituacao.Inativo.ToString().ToUpper()));

                return new ResponseProduto
                {
                    Sucesso = true,
                    TotalAtivos = totaisAtivos,
                    TotalInativos = totaisInativos
                };
            }
            catch (Exception ex)
            {
                return new ResponseProduto { Sucesso = false, Mensagem = ex.Message };
            }
        }

        private List<Produtos> FiltrarLista(RequestProduto request)
        {

            var naoTemDescricao = string.IsNullOrWhiteSpace(request.Descricao);
            var naoTemCodigoBarra = string.IsNullOrEmpty(request.CodigoBarra);
            return _produtosRepository.ObterPor(x => naoTemCodigoBarra
                                                       ? x.CodigoBarra != null
                                                       : x.CodigoBarra == request.CodigoBarra
                                                      && naoTemDescricao
                                                        ? x.Descricao != null
                                                        : x.Descricao == request.Descricao
                                                      && request.SubGrupoProdutoId == 0
                                                        ? x.SubGrupoProdutoId > 0
                                                        : x.SubGrupoProdutoId == request.SubGrupoProdutoId).ToList();

        }

        private void ValidarCadastro(Produtos produto, bool cadastrar = false)
        {
            var lista = _produtosRepository.ObterTodos().ToList();
            var request = new RequestProdutoBusiness
            {
                Produto = produto,
                ListaProdutos = lista,
                Cadastrar = cadastrar
            };

            _produtoBusiness.ValidarCadastro(request);
        }

        internal void AtualizarValorAtacado(List<ItensCompra> itens)
        {
            foreach (var item in itens)
            {
                var produto = _produtosRepository.Obter(item.ProdutoId);

                produto.ValorAtacado = item.ValorItem.Value;
                produto.CalcularPorcentagemVenda();

                _produtosRepository.Alterar(produto);
                _produtosRepository.SaveChanges();
            }

        }
    }
}
