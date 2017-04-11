namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.Cliente;
    using Application.Services.Produto;
    using Application.Services.Vendas;
    using Domain.Model;
    using HtmlPdfReport;
    using Models.Clientes;
    using Models.Produtos;
    using Models.Vendas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class VendasController : PdfViewController
    {
        private VendasAppService _vendasAppService;
        private ProdutoAppService _produtoAppService;
        private ClienteAppService _clienteAppService;

        public VendasController(VendasAppService vendasAppService, ProdutoAppService produtoAppService, ClienteAppService clienteAppService)
        {
            _vendasAppService = vendasAppService;
            _clienteAppService = clienteAppService;
            _produtoAppService = produtoAppService;
        }

        public ActionResult Index()
        {
            Session["LISTA-ITENS-VENDA"] = null;
            return View(new VendasViewModel { ListaClientes = PreencherClientes(), Quantidade = 1 });
        }

        [HttpPost]
        public ActionResult Index(VendasViewModel viewModel)
        {
            ModelState.Clear();
            if (Session["LISTA-ITENS-VENDA"] == null)
            {
                TempData.Add("Alerta", "Favor inserir itens para venda.");
                viewModel.ListaClientes = PreencherClientes();
                viewModel.Quantidade = 1;
                return View(viewModel);
            }

            var itensVenda = ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]).Select(x => new ItensVenda
            {
                ProdutoId = x.Id,
                Quantidade = x.Quantidade,
                ValorItem = x.ValorItem
            }).ToList();

            var resposta = _vendasAppService.Cadastrar(new RequestVendas
            {
                ClienteId = viewModel.ClienteId,
                ItensVenda = itensVenda
            });

            if (!resposta.Sucesso)
            {
                TempData.Add("Alerta", resposta.Mensagem);
                viewModel.ListaClientes = PreencherClientes();
                viewModel.Quantidade = 1;
                return View(viewModel);
            }

            TempData.Add("Sucesso", resposta.Mensagem);
            var cliente = PreencherClientes().FirstOrDefault(x => x.Id == viewModel.ClienteId);
            var model = new VendasViewModel
            {
                ListaVendas = (List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"],
                DataVenda = DateTime.Now.ToShortDateString(),
                CpfCnpjCLiente = cliente.Cpf,
                TelefoneCliente = cliente.FoneMovel1,
                ClienteDescricao = cliente.Nome
            };

            model.SubTotal = model.ListaVendas.Sum(x => x.TotalItem).ToString("N2");
            FillImageUrl(model, "report.jpg");
            return ViewPdf("", "PedidoVenda", model);

            //return RedirectToAction("Index", "Vendas");
        }

        [HttpPost]
        public JsonResult ObterInserirItemGrid(string quantidade, string codigoProduto)
        {
            VendasViewModel view = new VendasViewModel();
            if (Session["LISTA-ITENS-VENDA"] == null)
                Session["LISTA-ITENS-VENDA"] = new List<VendasItensViewModel>();

            view.ListaVendas = ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]);
            if (view.ListaVendas.Any(x => x.CodigoBarra == codigoProduto))
                return Json(new { data = "Erro" });

            var qtd = string.IsNullOrWhiteSpace(quantidade) ? 0 : Convert.ToInt32(quantidade);
            var resposta = _produtoAppService.ObterProduto(codigoProduto);

            if (resposta.Sucesso && qtd > 0)
            {
                if (qtd > resposta.Produto.Estoque.Quantidade)
                    return Json(new { data = "Erro", message = "Quantidade insuficiente em estoque." });

                view.ListaVendas.Add(new VendasItensViewModel
                {
                    Id = resposta.Produto.Id,
                    CodigoBarra = resposta.Produto.CodigoBarra.Trim(),
                    Descricao = resposta.Produto.Descricao,
                    Quantidade = qtd,
                    ValorItem = resposta.Produto.ValorVenda,
                    TotalItem = qtd * resposta.Produto.ValorVenda
                });
            }

            view.SubTotal = view.ListaVendas.Sum(x => x.TotalItem).ToString("N2");
            view.Quantidade = 1;

            Session["LISTA-ITENS-VENDA"] = view.ListaVendas;
            return Json(new { data = view });
        }

        [HttpPost]
        public JsonResult RemoverItemGrid(string id)
        {
            var item = ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]).FirstOrDefault(x => x.CodigoBarra == id);
            ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]).Remove(item);

            return Json(new VendasViewModel
            {
                ListaVendas = ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]),
                SubTotal = ((List<VendasItensViewModel>)Session["LISTA-ITENS-VENDA"]).Sum(x => x.TotalItem).ToString("N2"),
                Quantidade = 1
            });
        }

        public ActionResult Consultar()
        {
            return View(new VendasViewModel { ListaClientes = PreencherClientes() });
        }

        public ActionResult Imprimir(int id)
        {
            var resposta = _vendasAppService.ObterVenda(id);

            VendasViewModel model = new VendasViewModel
            {
                Id = resposta.Venda.Id,
                SubTotal = resposta.Venda.ItensVenda.Sum(x => x.Quantidade * x.ValorItem).ToString("N2"),
                ClienteDescricao = resposta.Venda.Cliente.Nome,
                CpfCnpjCLiente = resposta.Venda.Cliente.Cpf,
                TelefoneCliente = resposta.Venda.Cliente.FoneMovel1,
                DataVenda = resposta.Venda.DataVenda.ToShortDateString(),
                ListaVendas = resposta.Venda.ItensVenda.Select(x => new VendasItensViewModel
                {
                    CodigoBarra = x.Produtos.CodigoBarra,
                    Descricao = x.Produtos.Descricao,
                    Quantidade = x.Quantidade,
                    ValorItem = x.ValorItem,
                    TotalItem = x.Quantidade * x.ValorItem
                }).ToList()
            };

            FillImageUrl(model, "report.jpg");
            return ViewPdf("", "PedidoVenda", model);

        }

        [HttpPost]
        public JsonResult ObterItensGrid(VendasViewModel viewModel)
        {
            var resposta = _vendasAppService.ListarVendas(new RequestVendas
            {
                DataFim = viewModel.DataFim,
                DataInicio = viewModel.DataInicio,
                ClienteId = viewModel.ClienteId
            });

            var lista = resposta.ListaVendas.GroupBy(x => new { x.Id, x.DataVenda, x.Cliente.Nome, x.ValorVenda, x.ItensVenda })
                .Select(x => new VendasViewModel
                {
                    Id = x.Key.Id,
                    DataVenda = x.Key.DataVenda.ToShortDateString(),
                    ClienteDescricao = x.Key.Nome.Trim(),
                    SubTotal = x.Key.ValorVenda.ToString("N2"),
                    ListaVendas = x.Key.ItensVenda.Select(c => new VendasItensViewModel
                    {
                        Descricao = c.Produtos.ObterDescricao(),
                        Quantidade = c.Quantidade,
                        ValorItem = c.ValorItem,
                        TotalItem = c.Quantidade * c.ValorItem
                    }).ToList()
                });

            return Json(new { data = lista });
        }

        private List<ProdutosItensViewModel> PreencherProdutos()
        {
            return _produtoAppService.ObterListaAtivos().ListaProdutos.Select(x => new ProdutosItensViewModel
            {
                Id = x.Id,
                Descricao = $"{x.CodigoBarra.Trim()} - {x.Descricao.Trim()}"
            }).ToList();
        }

        private List<ClienteItemViewModel> PreencherClientes()
        {
            return _clienteAppService.ConsultarAtivos().ListaCliente.Select(x => new ClienteItemViewModel
            {
                Id = x.Id,
                Nome = x.Nome.Trim()
            }).ToList();
        }

        private void FillImageUrl(VendasViewModel customerList, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            customerList.ImageUrl = url + "Content/" + imageName;
        }
    }
}