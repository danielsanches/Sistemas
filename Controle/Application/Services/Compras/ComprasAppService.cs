namespace Application.Services.Compras
{
    using Domain.Business.Compras;
    using Domain.Enum;
    using Domain.Interfaces;
    using Domain.Model;
    using Estoque;
    using Produto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;

    public class ComprasAppService
    {
        private readonly ComprasBusiness _comprasBusiness;
        private readonly IRepository<Compra> _compraRepository;
        private readonly EstoqueAppService _estoqueAppService;
        private readonly ProdutoAppService _produtoAppService;

        public ComprasAppService(IRepository<Compra> compraRepository, ComprasBusiness comprasBusiness,
            EstoqueAppService estoqueAppService, ProdutoAppService produtoAppService)
        {
            _comprasBusiness = comprasBusiness;
            _compraRepository = compraRepository;
            _estoqueAppService = estoqueAppService;
            _produtoAppService = produtoAppService;
        }

        public ResponseCompra Cadastrar(Compra compra)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    _comprasBusiness.ValidarCompra(compra);
                    _compraRepository.Cadastrar(compra);

                    foreach (ItensCompra item in compra.ItensCompra)
                    {
                        _estoqueAppService.LancarEstoque(new Estoque
                        {
                            ProdutoId = item.ProdutoId,
                            Quantidade = item.Quantidade
                        });
                    }

                    _compraRepository.SaveChanges();
                    _estoqueAppService.Salvar();
                    _produtoAppService.AtualizarValorAtacado(compra.ItensCompra.ToList());

                    trans.Complete();
                    return new ResponseCompra { Sucesso = true, Mensagem = "Compra lançada com sucesso." };
                }
                catch (Exception ex)
                {
                    return new ResponseCompra { Sucesso = false, Mensagem = ex.Message };
                }
            }
        }

        public ResponseCompra Alterar(Compra compra)
        {
            try
            {
                var query = _compraRepository.Obter(compra.Id);
                if (query == null)
                    throw new Exception("Compra não encontrada.");

                query.Status = EnumSituacao.Inativo.ToString();
                query.DataAlteracao = DateTime.Now;

                _comprasBusiness.ValidarCompra(compra);

                List<Estoque> listaEstoque = query.ItensCompra.Select(x => new Estoque
                {
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade
                }).ToList();

                _estoqueAppService.BaixarEstoque(listaEstoque);

                foreach (ItensCompra item in compra.ItensCompra)
                {
                    _estoqueAppService.LancarEstoque(new Estoque
                    {
                        ProdutoId = item.ProdutoId,
                        Quantidade = item.Quantidade
                    });
                }

                _compraRepository.Alterar(query);
                _compraRepository.SaveChanges();
                _estoqueAppService.Salvar();
                _produtoAppService.AtualizarValorAtacado(compra.ItensCompra.ToList());

                return new ResponseCompra { Sucesso = true, Mensagem = "Compra alterada com sucesso." };
            }
            catch (Exception ex)
            {

                return new ResponseCompra { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCompra Consultar(RequestCompra request)
        {
            try
            {
                DateTime? dataFinal = null;
                if (request.DataInicial.HasValue && request.DataFinal.HasValue)
                    dataFinal = Convert.ToDateTime(request.DataFinal.Value.ToShortDateString() + " 23:59:59");

                List<Compra> lista = _compraRepository.ObterPor(x => dataFinal.HasValue
                                                        ? x.DataCompra >= request.DataInicial && x.DataCompra <= dataFinal.Value
                                                        : x.DataCompra.HasValue
                                                       && request.FornecedorId.HasValue
                                                        ? x.FornecedorId == request.FornecedorId
                                                        : x.FornecedorId.HasValue).ToList();

                return new ResponseCompra { Sucesso = true, ListaCompras = lista };
            }
            catch (Exception ex)
            {
                return new ResponseCompra { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseCompra Remover(int id)
        {
            try
            {
                Compra compra = _compraRepository.Obter(id);
                if (compra == null)
                    throw new Exception("Compra não encontrada.");

                List<Estoque> listaEstoque = new List<Estoque>();
                foreach (ItensCompra item in compra.ItensCompra)
                {
                    listaEstoque.Add(new Estoque
                    {
                        ProdutoId = item.ProdutoId,
                        Quantidade = item.Quantidade
                    });
                }

                _estoqueAppService.BaixarEstoque(listaEstoque);
                _compraRepository.Remover(id);
                _compraRepository.SaveChanges();
                _estoqueAppService.Salvar();

                return new ResponseCompra { Sucesso = true, Mensagem = "Compra removida com sucesso." };
            }
            catch (Exception ex)
            {
                return new ResponseCompra { Sucesso = false, Mensagem = ex.Message };
            }
        }
    }
}
