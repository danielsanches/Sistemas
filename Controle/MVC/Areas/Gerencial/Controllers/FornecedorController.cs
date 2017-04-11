namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.Fornecedor;
    using Models.Fornecedor;
    using System;
    using System.Linq;
    using System.Web.Mvc;


    [Authorize]
    public class FornecedorController : Controller
    {
        private readonly FornecedorAppService _fornecedorAppService;

        public FornecedorController(FornecedorAppService fornecedorAppService)
        {
            _fornecedorAppService = fornecedorAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar(string id)
        {
            return View(new FornecedorViewModel());
        }

        [HttpPost]
        public ActionResult Cadastrar(FornecedorViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _fornecedorAppService.Cadastrar(new RequestFornecedor
            {
                NomeFantazia = viewModel.NomeFantasia,
                Celular = viewModel.Celular,
                Email = viewModel.Email,
                FoneFixo = viewModel.FoneFixo
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mesagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mesagem, resposta.Sucesso);
            return RedirectToAction("Index", "Fornecedor");
        }

        public ActionResult Alterar(string id)
        {
            var resposta = _fornecedorAppService.Obter(Convert.ToInt32(id));
            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mesagem, resposta.Sucesso);
                return RedirectToAction("Index", "Fornecedor");
            }

            var viewModel = new FornecedorViewModel
            {
                Id = resposta.Fornecedor.Id,
                NomeFantasia = resposta.Fornecedor.NomeFantasia,
                Celular = resposta.Fornecedor.Celular,
                Email = resposta.Fornecedor.Email,
                FoneFixo = resposta.Fornecedor.FoneFixo,
                Status = resposta.Fornecedor.Status.Equals("Ativo")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(FornecedorViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _fornecedorAppService.Alterar(new RequestFornecedor
            {
                Id = viewModel.Id,
                NomeFantazia = viewModel.NomeFantasia,
                Celular = viewModel.Celular,
                Email = viewModel.Email,
                FoneFixo = viewModel.FoneFixo,
                Status = viewModel.Status ? "Ativo" : "Inativo"
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mesagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mesagem, resposta.Sucesso);
            return RedirectToAction("Index", "Fornecedor");
        }

        [HttpPost]
        public JsonResult ObterItensGrid(string descricao, string situacao)
        {
            var resposta = _fornecedorAppService.ObterLista(new RequestFornecedor
            {
                NomeFantazia = descricao,
                Status = situacao
            });

            var lista = resposta.ListaFornecedores.Select(x => new FornecedorItemViewModel
            {
                Id = x.Id,
                Celular = x.Celular,
                DataCadastro = x.DataCadastro,
                Email = x.Email,
                FoneFixo = x.FoneFixo,
                NomeFantasia = x.NomeFantasia,
                Status = x.Status
            });

            var jsonData = new { data = lista };

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult ObterQuantidadeAbas(string descricao, string situacao)
        {
            var resposta = _fornecedorAppService.ObterQuantidades(new RequestFornecedor
            {
                NomeFantazia = descricao,
                Status = situacao
            });

            var inativo = resposta.QtdInativos;
            var ativo = resposta.QtdAtivos;

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