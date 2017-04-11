namespace MVC
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Account",
                    action = "Logar",
                    id = UrlParameter.Optional,
                    language = "pt",
                    culture = "BR"
                }
            );
        }
    }
}
