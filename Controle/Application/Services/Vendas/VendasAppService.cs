namespace Application.Services.Vendas
{
    using Domain.Business.Vendas;
    using Domain.Interfaces;
    using Domain.Model;
    using Estoque;
    using System;
    using System.Linq;
    using Infra.Reposiotry;

    public class VendasAppService
    {
        private readonly IRepository<Vendas> _vendasRepository;
        private readonly VendasBusiness _vendasBusiness;
        private readonly EstoqueAppService _estoqueAppService;

        public VendasAppService()
        {
            _vendasRepository = new VendasRepository();
            _vendasBusiness = new VendasBusiness();
            _estoqueAppService = new EstoqueAppService();
        }

        public ResponseVendas Cadastrar(RequestVendas request)
        {
            try
            {
                var venda = new Vendas
                {
                    ClienteId = request.ClienteId,
                    ItensVenda = request.ItensVenda,
                    ValorVenda = request.ItensVenda.Sum(x => x.Quantidade * x.ValorItem),
                    Status = "Ativo",
                    DataVenda = DateTime.Now
                };

                _vendasBusiness.ValidarLancamento(venda);

                _estoqueAppService.BaixarEstoque(venda.ItensVenda.Select(x => new Estoque
                {
                    ProdutoId = x.ProdutoId,
                    Quantidade = x.Quantidade
                }).ToList());

                _vendasRepository.Cadastrar(venda);
                _vendasRepository.SaveChanges();
                _estoqueAppService.Salvar();

                return new ResponseVendas { Sucesso = true, Mensagem = "Venda concluída com sucesso.", VendaId = venda.Id };
            }
            catch (Exception ex)
            {
                return new ResponseVendas { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseVendas ObterVenda(int idVenda)
        {
            try
            {
                var venda = _vendasRepository.Obter(idVenda);
                if (venda == null)
                    throw new Exception("Registro de venda não encontrado.");

                return new ResponseVendas { Sucesso = true, Venda = venda };
            }
            catch (Exception ex)
            {
                return new ResponseVendas { Sucesso = false, Mensagem = ex.Message };
            }
        }

        public ResponseVendas ListarVendas(RequestVendas request)
        {
            try
            {
                DateTime? dataFinal = null;
                if (request.DataInicio.HasValue && request.DataFim.HasValue)
                    dataFinal = Convert.ToDateTime(request.DataFim.Value.ToShortDateString() + " 23:59:59");

                var temDescricao = request.ClienteId > 0;

                var lista = _vendasRepository.ObterPor(x => dataFinal.HasValue
                                                        ? x.DataVenda >= request.DataFim && x.DataVenda <= dataFinal.Value
                                                        : x.DataVenda != null
                                                        && temDescricao
                                                        ? x.Cliente.Id == request.ClienteId
                                                        : x.Cliente != null).ToList();

                return new ResponseVendas { Sucesso = true, ListaVendas = lista };
            }
            catch (Exception ex)
            {
                return new ResponseVendas { Sucesso = false, Mensagem = ex.Message };
            }
        }
    }
}
