namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.GrupoProduto;
    using Application.Services.Produto;
    using Application.Services.SubGrupoProduto;
    using Models.GrupoProduto;
    using Models.Produtos;
    using Models.SubGrupoProduto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;


    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ProdutoAppService _produtoAppService;
        private readonly SubGrupoProdutoAppService _subGrupoProdutoAppService;
        private readonly GrupoProdutoAppService _grupoProdutoAppService;

        public ProdutosController()
        {
            _produtoAppService = new ProdutoAppService();
            _subGrupoProdutoAppService = new SubGrupoProdutoAppService();
            _grupoProdutoAppService = new GrupoProdutoAppService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            var viewModel = ResetarPagina();
            viewModel.ListaSubGrupo = new List<SubGrupoProdutoViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Cadastrar(ProdutosViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _produtoAppService.Cadastrar(new RequestProduto
            {
                Descricao = viewModel.Descricao,
                CodigoBarra = viewModel.CodigoBarra,
                UidadeArmazenamento = viewModel.UidadeArmazenamento,
                SubGrupoProdutoId = Convert.ToInt32(viewModel.SubGrupoProduto),
                PercentualVenda = viewModel.PercentualVenda
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                viewModel.ListaSubGrupo = ObterListaSubGrupo();
                viewModel.ListaGrupoProduto = ObterListaGrupoProduto();
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return View(ResetarPagina());
        }

        public ActionResult Alterar(string id)
        {
            var resposta = _produtoAppService.Obter(Convert.ToInt32(id));
            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return RedirectToAction("Index", "Produtos");
            }

            var viewModel = ResetarPagina();

            viewModel.Descricao = resposta.Produto.Descricao;
            viewModel.Id = resposta.Produto.Id;
            viewModel.CodigoBarra = resposta.Produto.CodigoBarra;
            viewModel.Quantidade = resposta.Produto.Quantidade;
            viewModel.Status = resposta.Produto.Status.Equals("Ativo");
            viewModel.UidadeArmazenamento = resposta.Produto.UnidadeArmazenamento;
            viewModel.PercentualVenda = resposta.Produto.PercentualVenda;
            viewModel.GrupoProduto = resposta.Produto.SubGrupoProduto.GrupoProdutoId.ToString();
            viewModel.SubGrupoProduto = resposta.Produto.SubGrupoProdutoId.ToString();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(ProdutosViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _produtoAppService.Alterar(new RequestProduto
            {
                Id = viewModel.Id,
                Descricao = viewModel.Descricao,
                CodigoBarra = viewModel.CodigoBarra,
                Status = viewModel.Status ? "Ativo" : "Inativo",
                UidadeArmazenamento = viewModel.UidadeArmazenamento,
                PercentualVenda = viewModel.PercentualVenda
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                viewModel.ListaSubGrupo = ObterListaSubGrupo();
                viewModel.ListaGrupoProduto = ObterListaGrupoProduto();
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "Produtos");
        }

        [HttpPost]
        public JsonResult ObterProdutosGrid(string descricao, string codigo, string situacao)
        {
            var resposta = _produtoAppService.ObterLista(new RequestProduto
            {
                Descricao = descricao,
                CodigoBarra = codigo,
                Status = situacao
            });

            var lista = resposta.ListaProdutos.Select(x => new ProdutosItensViewModel
            {
                CodigoBarra = x.CodigoBarra,
                Id = x.Id,
                Descricao = x.Descricao,
                ValorAtacado = x.ValorAtacado.ToString("C2"),
                ValorVenda = x.ValorVenda.ToString("C2"),
                PercentualVenda = $"% {Math.Round(x.PercentualVenda)}"
            }).ToList();

            var jsonData = new { data = lista };

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult ObterQuantidadeAbas(string descricao, string codigo, string situacao)
        {
            var resposta = _produtoAppService.ObterTotaisProdutos(new RequestProduto
            {
                Descricao = descricao,
                CodigoBarra = codigo,
                Status = situacao
            });

            var inativo = resposta.TotalInativos;
            var ativo = resposta.TotalAtivos;

            return Json(new { ativo = ativo, inativo = inativo, });
        }

        [HttpPost]
        public JsonResult ObterListaComboSubGrupoGrupo(string id)
        {
            return Json(ObterListaSubGrupo().Where(x => x.GrupoId == id).ToList());
        }

        private ProdutosViewModel ResetarPagina()
        {
            return new ProdutosViewModel
            {
                ListaSubGrupo = ObterListaSubGrupo(),
                ListaGrupoProduto = ObterListaGrupoProduto()
            };
        }

        private List<SubGrupoProdutoViewModel> ObterListaSubGrupo()
        {
            var resposta = _subGrupoProdutoAppService.ObterListaAtivos();
            return resposta.ListaSubGrupoProduto.Select(x => new SubGrupoProdutoViewModel
            {
                Id = x.Id,
                GrupoId = x.GrupoProdutoId.HasValue ? x.GrupoProdutoId.Value.ToString() : string.Empty,
                Descricao = x.Descricao
            }).ToList();
        }

        private List<GrupoProdutoViewModel> ObterListaGrupoProduto()
        {
            var resposta = _grupoProdutoAppService.ObterListaAtivos();
            return resposta.ListaGrupoProduto.Select(x => new GrupoProdutoViewModel
            {
                Id = x.Id,
                Descricao = x.Descricao
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