namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.Cliente;
    using Models.Clientes;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class ClientesController : Controller
    {
        public ClienteAppService _clienteAppService;

        public ClientesController()
        {
            _clienteAppService = new ClienteAppService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public ActionResult Cadastrar(ClienteViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _clienteAppService.Cadastrar(new RequestCliente
            {
                Cliente = new Domain.Model.Cliente
                {
                    Nome = viewModel.Nome,
                    Cpf = viewModel.Cpf,
                    DataNascimento = viewModel.DataNascimento,
                    Email = viewModel.Email,
                    FoneFixo = viewModel.FoneFixo,
                    FoneMovel1 = viewModel.FoneMovel1,
                    FoneMovel2 = viewModel.FoneMovel2
                }
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "Clientes");
        }

        public ActionResult Alterar(string id)
        {
            var resposta = _clienteAppService.Obter(Convert.ToInt32(id));
            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return RedirectToAction("Index", "Clientes");
            }

            var viewModel = new ClienteViewModel
            {
                Id = resposta.Cliente.Id,
                Cpf = resposta.Cliente.Cpf,
                DataNascimento = resposta.Cliente.DataNascimento,
                DataCadastro = resposta.Cliente.DataCadastro,
                Email = resposta.Cliente.Email,
                FoneFixo = resposta.Cliente.FoneFixo,
                FoneMovel1 = resposta.Cliente.FoneMovel1,
                FoneMovel2 = resposta.Cliente.FoneMovel2,
                Nome = resposta.Cliente.Nome,
                Status = resposta.Cliente.Status.Equals("Ativo")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Alterar(ClienteViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _clienteAppService.Alterar(new RequestCliente
            {
                Id = viewModel.Id,
                Cliente = new Domain.Model.Cliente
                {
                    Nome = viewModel.Nome,
                    Cpf = viewModel.Cpf,
                    DataNascimento = viewModel.DataNascimento,
                    Email = viewModel.Email,
                    FoneFixo = viewModel.FoneFixo,
                    FoneMovel1 = viewModel.FoneMovel1,
                    FoneMovel2 = viewModel.FoneMovel2,
                    Status = viewModel.Status ? "Ativo" : "Inativo"
                }
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Index", "Clientes");
        }

        [HttpPost]
        public JsonResult ObterItensGrid(string descricao, string aniverssario, string situacao)
        {
            var resposta = _clienteAppService.ObterLista(new RequestCliente
            {
                Cliente = new Domain.Model.Cliente
                {
                    Nome = descricao,
                    DataNascimento = string.IsNullOrWhiteSpace(aniverssario) ? (DateTime?)null : Convert.ToDateTime("01/" + aniverssario),
                    Status = situacao
                }
            });

            var lista = resposta.ListaCliente.Select(x => new ClienteViewModel
            {
                Id = x.Id,
                Cpf = x.Cpf,
                DataCadastro = x.DataCadastro,
                DataNascimento = x.DataNascimento,
                Email = x.Email,
                FoneFixo = x.FoneFixo,
                FoneMovel1 = x.FoneMovel1,
                FoneMovel2 = x.FoneMovel2,
                Nome = x.Nome
            }).ToList();

            var jsonData = new { data = lista };

            return Json(jsonData);
        }

        [HttpPost]
        public JsonResult ObterQuantidadeAbas(string descricao, string aniverssario, string situacao)
        {
            var resposta = _clienteAppService.ObterQuantidades(new RequestCliente
            {
                Cliente = new Domain.Model.Cliente
                {
                    Nome = descricao,
                    DataNascimento = string.IsNullOrWhiteSpace(aniverssario) ? (DateTime?)null : Convert.ToDateTime(aniverssario),
                    Status = situacao
                }
            });

            var inativo = resposta.QtdInativo;
            var ativo = resposta.QtdAtivo;

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