namespace MVC
{
    using Domain.Interfaces;
    using Domain.Model;
    using Infra.Reposiotry;
    using ModelBinder;
    using Ninject;
    using Ninject.Web.Mvc;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());

            DependencyResolver.Resolver();
        }
    }

    public  class DependencyResolver
    {
        public static void Resolver()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IRepository<Usuario>>().To<UsuarioRepository>();
            kernel.Bind<IRepository<Cliente>>().To<ClienteRepository>();
            kernel.Bind<IRepository<Fornecedor>>().To<FornecedorRepository>();
            kernel.Bind<IRepository<Produtos>>().To<ProdutosRepository>();
            kernel.Bind<IRepository<GrupoProduto>>().To<GrupoProdutoRepository>();
            kernel.Bind<IRepository<SubGrupoProduto>>().To<SubGrupoProdutoRepository>();
            kernel.Bind<IRepository<Estoque>>().To<EstoqueRepository>();
            kernel.Bind<IRepository<Compra>>().To<CompraRepository>();
            kernel.Bind<IRepository<Vendas>>().To<VendasRepository>();

            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }        
    }
}
