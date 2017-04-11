namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.GrupoProduto;
    using Models.GrupoProduto;
    using System;
    using System.Linq;
    using System.Web.Mvc;


    [Authorize]
    public class GrupoProdutoController : Controller
    {
        private readonly GrupoProdutoAppService _grupoProdutoAppService;

        public GrupoProdutoController(GrupoProdutoAppService grupoProdutoAppService)
        {
            _grupoProdutoAppService = grupoProdutoAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View(new GrupoProdutoViewModel());
        }

        [HttpPost]
        public ActionResult Cadastrar(GrupoProdutoViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _grupoProdutoAppService.Cadastrar(new RequestGrupoProduto
            {
                Descricao = viewModel.Descricao
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return View(new GrupoProdutoViewModel());
        }

        public ActionResult Alterar(string id)
        {
            var resposta = _grupoProdutoAppService.Obter(Convert.ToInt32(id));
            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return RedirectToAction("Index", "GrupoProduto");
            }

            var viewModel = new GrupoProdutoViewModel
            {
                Descricao = resposta.GrupoProduto.Descricao,
                Id = resposta.GrupoProduto.Id,
                Status = resposta.GrupoProduto.Status.Equals("Ativo")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(GrupoProdutoViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _grupoProdutoAppService.Alterar(new RequestGrupoProduto
            {
                Id = viewModel.Id,
                Descricao = viewModel.Descricao,
                Situacao = viewModel.Status ? "Ativo" : "Inativo"
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "GrupoProduto");
        }

        [HttpPost]
        public JsonResult ObterGruposGrid(string descricao, string situacao)
        {
            var resposta = _grupoProdutoAppService.ObterLista(new RequestGrupoProduto
            {
                Descricao = descricao,
                Situacao = situacao
            });

            var lista = resposta.ListaGrupoProduto.Select(x => new GrupoProdutoViewModel
            {
                Id = x.Id,
                Descricao = x.Descricao
            }).ToList();

            return Json(new { data = lista });
        }

        [HttpPost]
        public JsonResult ObterQuantidadeAbas(string descricao, string situacao)
        {
            var resposta = _grupoProdutoAppService.ObterTotaisProdutos(new RequestGrupoProduto
            {
                Descricao = descricao,
                Situacao = situacao
            });

            var inativo = resposta.TotalInativos;
            var ativo = resposta.TotalAtivos;

            return Json(new { ativo = ativo, inativo = inativo, });
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