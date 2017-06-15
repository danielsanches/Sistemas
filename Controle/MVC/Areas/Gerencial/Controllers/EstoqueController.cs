namespace MVC.Areas.Gerencial.Controllers
{
    using Application.Services.Estoque;
    using Application.Services.Produto;
    using Models.Estoque;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize]
    public class EstoqueController : Controller
    {
        private readonly EstoqueAppService _estoqueAppService;
        private readonly ProdutoAppService _produtoAppService;

        public EstoqueController()
        {
            _estoqueAppService = new EstoqueAppService();
            _produtoAppService = new ProdutoAppService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObterItensGrid()
        {
            var resposta = _estoqueAppService.ObterListaEstoque();
            var lista = resposta.ListaEstoque.Select(x => new EstoqueViewModel
            {
                ProdutoId = x.ProdutoId,
                CodigoBarra = _produtoAppService.Obter(x.ProdutoId).Produto.CodigoBarra,
                Descricao = _produtoAppService.Obter(x.ProdutoId).Produto.Descricao,
                Quantidade = x.Quantidade
            }).ToList();

            var jsonData = new { data = lista };
            return Json(jsonData);
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