namespace MVC.Controllers
{
    using Application.Services.UsuarioLogin;
    using Models;
    using System.Web.Mvc;
    using System.Web.Security;

    public class AccountController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public AccountController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public ActionResult Logar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logar(AccountViewModel viewModel)
        {
            ModelState.Clear();
            var resposta = _usuarioService.Logar(new RequestUsuario
            {
                NomeLogin = viewModel.NomeLogin,
                Senha = viewModel.Senha
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            FormsAuthentication.SetAuthCookie(viewModel.NomeLogin?.Trim(), false);

            Session["NomeMenu"] = resposta.Usuario.Nome;
            Session["EmailMenu"] = resposta.Usuario.Email;
            Session["NomeLoginMenu"] = resposta.Usuario.NomeLogin;
            return RedirectToAction("Index", "Vendas", new { area = "Gerencial" });
        }

        [Authorize]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(AccountViewModel viewModel)
        {
            var resposta = _usuarioService.Cadastrar(new RequestUsuario
            {
                Nome = viewModel.Nome,
                NomeLogin = viewModel.NomeLogin,
                Senha = viewModel.Senha,
                Email = viewModel.Email,
                ConfirmaSenha = viewModel.ConfirmaSenha,
            });

            if (!resposta.Sucesso)
            {
                AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
                return View(viewModel);
            }

            AdicionarMensagem(resposta.Mensagem, resposta.Sucesso);
            return RedirectToAction("Logar", "Account");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Logar", "Account");
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