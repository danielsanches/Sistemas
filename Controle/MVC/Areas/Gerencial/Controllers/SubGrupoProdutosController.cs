namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.GrupoProduto;
    using Application.Services.SubGrupoProduto;
    using Models.GrupoProduto;
    using Models.SubGrupoProduto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class SubGrupoProdutosController : Controller
    {
        private readonly SubGrupoProdutoAppService _subGrupoProdutoAppService;
        private readonly GrupoProdutoAppService _grupoProdutoAppService;

        public SubGrupoProdutosController()
        {
            _subGrupoProdutoAppService = new SubGrupoProdutoAppService();
            _grupoProdutoAppService = new GrupoProdutoAppService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View(new SubGrupoProdutoViewModel { ListaGrupoProdutos = ObterGruposProduto() });
        }

        [HttpPost]
        public ActionResult Cadastrar(SubGrupoProdutoViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _subGrupoProdutoAppService.Cadastrar(new RequestSubGrupoProduto
            {
                Descricao = viewModel.Descricao,
                GrupoProdutoId = string.IsNullOrWhiteSpace(viewModel.GrupoId) ? (int?)null : Convert.ToInt32(viewModel.GrupoId)
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                viewModel.ListaGrupoProdutos = ObterGruposProduto();
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return View(new SubGrupoProdutoViewModel { ListaGrupoProdutos = ObterGruposProduto() });
        }

        public ActionResult Alterar(string id)
        {
            var resposta = _subGrupoProdutoAppService.Obter(Convert.ToInt32(id));
            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return RedirectToAction("Index", "SubGrupoProdutos");
            }

            var viewModel = new SubGrupoProdutoViewModel
            {
                Id = resposta.SubGrupoProduto.Id,
                Descricao = resposta.SubGrupoProduto.Descricao,
                GrupoId = resposta.SubGrupoProduto.GrupoProdutoId.Value.ToString(),
                ListaGrupoProdutos = ObterGruposProduto(),
                Status = resposta.SubGrupoProduto.Status.Equals("Ativo")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(SubGrupoProdutoViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _subGrupoProdutoAppService.Alterar(new RequestSubGrupoProduto
            {
                Id = viewModel.Id,
                Descricao = viewModel.Descricao,
                GrupoProdutoId = Convert.ToInt32(viewModel.GrupoId),
                Status = viewModel.Status ? "Ativo" : "Inativo"
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                viewModel.ListaGrupoProdutos = ObterGruposProduto();
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "SubGrupoProdutos");
        }

        [HttpPost]
        public JsonResult ObterSubGruposGrid(string descricao, string situacao)
        {
            var resposta = _subGrupoProdutoAppService.ObterLista(new RequestSubGrupoProduto
            {
                Descricao = descricao,
                Status = situacao
            });

            var jsonData = new
            {
                data = resposta.ListaSubGrupoProduto.Select(x => new SubGrupoProdutoViewModel
                {
                    Descricao = x.Descricao,
                    Id = x.Id,
                    GrupoId = x.GrupoProdutoId.Value.ToString(),
                    DescricaoGrupo = x.GrupoProduto.Descricao
                }).ToList()
            };

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult ObterQuantidadeAbas(string descricao, string situacao)
        {
            var resposta = _subGrupoProdutoAppService.ObterTotaisProdutos(new RequestSubGrupoProduto
            {
                Descricao = descricao,
                Status = situacao
            });

            var inativo = resposta.TotalInativos;
            var ativo = resposta.TotalAtivos;

            return Json(new { ativo = ativo, inativo = inativo, });
        }

        private List<GrupoProdutoViewModel> ObterGruposProduto()
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