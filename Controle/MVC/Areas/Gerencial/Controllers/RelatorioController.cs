using System.Web.Mvc;

namespace MVC.Areas.Gerencial.Controllers
{
    [Authorize]
    public class RelatorioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}