namespace MVC.Areas.Gerencial
{
    using System.Web.Mvc;

    public class GerencialAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Gerencial"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Gerencial_default",
                "Gerencial/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}