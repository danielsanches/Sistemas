namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.Compras;
    using Application.Services.Fornecedor;
    using Application.Services.Produto;
    using Atributes;
    using Domain.Model;
    using Models.Compras;
    using Models.Fornecedor;
    using Models.Produtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class ComprasController : Controller
    {
        private readonly ComprasAppService _comprasAppService;
        private readonly FornecedorAppService _fornecedorAppService;
        private readonly ProdutoAppService _produtoAppService;

        public ComprasController()
        {
            _comprasAppService = new ComprasAppService();
            _fornecedorAppService = new FornecedorAppService();
            _produtoAppService = new ProdutoAppService();
        }

        public ActionResult Index()
        {
            return View(new ComprasViewModel
            {
                ListaFornecedores = PreencherListaFornecedor()
            });
        }

        public ActionResult Cadastrar()
        {
            return View(new ComprasViewModel
            {
                ListaFornecedores = PreencherListaFornecedor(),
                ListaProdutos = PreencherListaProdutos()
            });
        }

        [HttpPost]
        [MultiButton(Name = "Cadastrar", Value = "Salvar")]
        public ActionResult Salvar(ComprasViewModel viewModel)
        {
            ModelState.Clear();
            viewModel.ListaFornecedores = PreencherListaFornecedor();
            viewModel.ListaProdutos = PreencherListaProdutos();

            List<ItensCompra> itensCompra = viewModel.ListaProdutosCompra.Select(x => new ItensCompra
            {
                ProdutoId = x.ProdutoId,
                Quantidade = x.QtdProduto,
                ValorItem = string.IsNullOrWhiteSpace(x.ValorUn) ? 0.0m : Convert.ToDecimal(x.ValorUn)
            }).ToList();

            ResponseCompra resposta = _comprasAppService.Cadastrar(new Compra
            {
                DataCompra = viewModel.DataCompra,
                FornecedorId = viewModel.FornecedorId,
                ItensCompra = itensCompra,
                ValorCompra = itensCompra.Sum(x => x.ValorTotalItem)
            });

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            if (!resposta.Sucesso)
                return View(viewModel);

            return RedirectToAction("Index", "Compras");
        }

        [HttpPost]
        [MultiButton(Name = "Cadastrar", Value = "Inserir")]
        public ActionResult Inserir(ComprasViewModel viewModel)
        {
            ModelState.Clear();
            viewModel.ListaFornecedores = PreencherListaFornecedor();
            viewModel.ListaProdutos = PreencherListaProdutos();

            if (string.IsNullOrWhiteSpace(viewModel.Produto))
            {
                AdicionarMensagem("Favor selecionar um item para inserir.", false);
                return View(viewModel);
            }
            if (viewModel.QtdProduto <= 0)
            {
                AdicionarMensagem("Favor informar a quantidade do produto.", false);
                return View(viewModel);
            }
            if (viewModel.ValorProduto <= 0)
            {
                AdicionarMensagem("Favor informar o valor do produto.", false);
                return View(viewModel);
            }

            var produtoId = Convert.ToInt32(viewModel.Produto);
            var produtoCompra = new ComprasItensViewModel
            {
                ProdutoDescricao = PreencherListaProdutos().FirstOrDefault(x => x.Id == produtoId).Descricao,
                ProdutoId = produtoId,
                QtdProduto = viewModel.QtdProduto,
                ValorUn = viewModel.ValorProduto.ToString("N2"),
                ValorTotal = (viewModel.QtdProduto * viewModel.ValorProduto).ToString("N2")
            };

            viewModel.ListaProdutosCompra.Add(produtoCompra);
            return View(viewModel);
        }

        public ActionResult Remover(int id)
        {
            ResponseCompra resposta = _comprasAppService.Remover(id);
            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "Compras");
        }

        [HttpPost]
        public JsonResult ObterItensGrid(ComprasViewModel viewModel)
        {
            ResponseCompra resposta = _comprasAppService.Consultar(new RequestCompra
            {
                DataInicial = viewModel.DataInicio,
                DataFinal = viewModel.DataFim,
                FornecedorId = viewModel.FornecedorId
            });

            var lista = resposta.ListaCompras.Select(x => new ListaComprasViewModel
            {
                Id = x.Id,
                DataCompraFormatada = x.DataCompra.HasValue ? x.DataCompra.Value.ToShortDateString() : string.Empty,
                DataLancamentoFormatada = x.DataLancamento.ToShortDateString(),
                FornecedorDescricao = x.Fornecedor.NomeFantasia.Trim(),
                ValorCompra = x.ValorCompra.Value.ToString("N2"),
                ListaItens = x.ItensCompra.Select(c => new ListaComprasItensViewModel
                {
                    Descricao = c.Produtos.ObterDescricao(),
                    Quantidade = c.Quantidade.ToString(),
                    ValorUnitario = c.ValorItem?.ToString("N2"),
                    ValorTotal = c.ValorTotalItem?.ToString("N2")
                }).ToList()
            }).ToList();

            return Json(new { data = lista });
        }

        private List<FornecedorItemViewModel> PreencherListaFornecedor()
        {
            ResponseFornecedor resposta = _fornecedorAppService.ObterListaAtivos();
            return resposta.ListaFornecedores.Select(x => new FornecedorItemViewModel
            {
                Id = x.Id,
                NomeFantasia = x.NomeFantasia.Trim()
            }).ToList();
        }

        private List<ProdutosItensViewModel> PreencherListaProdutos()
        {
            ResponseProduto resposta = _produtoAppService.ObterListaAtivos();
            return resposta.ListaProdutos.Select(x => new ProdutosItensViewModel
            {
                Id = x.Id,
                Descricao = $"{x.CodigoBarra} - {x.Descricao.Trim()}"
            }).ToList();
        }

        private void AdicionarMensagem(string msg, bool sucesso)
        {
            if (sucesso)
            {
                if (!TempData.ContainsKey("Sucesso"))
                    TempData.Add("Sucesso", msg);
            }
            else
            {
                if (!TempData.ContainsKey("Alerta"))
                    TempData.Add("Alerta", msg);
            }
        }
    }
}