namespace Application.Services.Estoque
{
    using Domain.Business.Estoque;
    using System;
    using Domain.Model;
    using Domain.Interfaces;
    using System.Linq;
    using System.Collections.Generic;

    public class EstoqueAppService
    {
        private readonly EstoqueBusiness _estoqueBusiness;
        private readonly IRepository<Estoque> _estoqueRepository;

        public EstoqueAppService(IRepository<Estoque> estoqueRepository, EstoqueBusiness estoqueBusiness)
        {
            _estoqueBusiness = estoqueBusiness;
            _estoqueRepository = estoqueRepository;
        }

        internal void LancarEstoque(Estoque estoque)
        {
            Estoque query = _estoqueRepository.Obter(estoque.ProdutoId);
            if (query == null)
            {
                _estoqueBusiness.ValidarLancamento(estoque);
                _estoqueRepository.Cadastrar(estoque);
            }
            else
            {
                query.Quantidade += estoque.Quantidade;
                _estoqueBusiness.ValidarLancamento(query);

                _estoqueRepository.Alterar(query);
            }
        }

        internal void BaixarEstoque(List<Estoque> lista)
        {
            foreach (Estoque item in lista)
            {
                Estoque query = _estoqueRepository.Obter(item.ProdutoId);
                query.Quantidade -= item.Quantidade;
                _estoqueRepository.Alterar(query);
            }
        }

        public ResponseEstoque ObterEstoque(int produtoId)
        {
            try
            {
                Estoque estoque = _estoqueRepository.Obter(produtoId);
                if (estoque == null)
                    throw new Exception("Estoque não encontradao para o produto informado.");

                return new ResponseEstoque { Sucesso = true, Estoque = estoque };
            }
            catch (Exception ex)
            {
                return new ResponseEstoque { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseEstoque ObterListaEstoque()
        {
            try
            {
                List<Estoque> lista = _estoqueRepository.ObterTodos().ToList();

                return new ResponseEstoque { Sucesso = true, ListaEstoque = lista };
            }
            catch (Exception ex)
            {
                return new ResponseEstoque { Sucesso = false, Mensagem = ex.Message };
            }
        }

        internal void Salvar()
        {
            _estoqueRepository.SaveChanges();
        }
    }
}
