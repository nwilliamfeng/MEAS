using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MEAS.Binder;
using System.Web.Security;
using System.Security.Principal;

namespace MEAS
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (!FormsAuthentication.CookiesSupported)
                return;           
            string cookieName = FormsAuthentication.FormsCookieName;
            if (string.IsNullOrEmpty(cookieName))
                return;
            HttpCookie authCookie = Context.Request.Cookies[cookieName];
            if (authCookie == null)
                return;

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] roles = authTicket.UserData.Split(',');
            var userIdentity = new FormsIdentity(authTicket); //此处也可以用自定义的IIdentity实例代替，比如GenericIdentity
            Context.User = new GenericPrincipal(userIdentity, roles);    
        }

        protected void Application_Start()
        {
            DependencyResolverConfig.RegistInstances(); //di注册实例         
            AutoMapperMappingConfig.Configure(); //配置AutoMapper
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartBinder()); //Page 191
        }



        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError().GetBaseException();
            HttpException httpException = ex as HttpException;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        Response.Redirect("~/Error/NotFound");
                        return;
                    case 403:
                        Response.Redirect("~/Error/Unauthorized");
                        return;
                    case 500:
                        //  action = "HttpError500";
                        break;
                    default:
                        break;
                }
            }
            Controller controller = new Controllers.ErrorController();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = "Index";
            controller.ViewData.Model = new HandleErrorInfo(ex, " ", " ");
            Server.ClearError();
            ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(this.Context), routeData));
          
        }

        //private   void SetAutofacContainer()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterControllers(Assembly.GetExecutingAssembly());

        //    builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
        //    builder.RegisterAssemblyTypes(typeof(ProductService).Assembly) .Where(t => t.Name.EndsWith("Service")) .AsImplementedInterfaces().InstancePerRequest();
        //    builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();

        // //   builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
        // //.Where(t => t.Name.EndsWith("Authentication"))
        // //.AsImplementedInterfaces().InstancePerHttpRequest();

        //    //builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SocialGoalEntities())))
        //    //    .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

        //    builder.RegisterFilterProvider();
        //    IContainer container = builder.Build();
        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}

        //private void InitizeAutofac()
        //{
        //    var builder = new ContainerBuilder();

        //    // Register your MVC controllers. (MvcApplication is the name of
        //    // the class in Global.asax.)
        //    builder.RegisterControllers(Assembly.GetExecutingAssembly());
           
        //    //// OPTIONAL: Register web abstractions like HttpContextBase.
        //    //builder.RegisterModule<AutofacWebTypesModule>();

        //    //// OPTIONAL: Enable property injection in view pages.
        //    //builder.RegisterSource(new ViewRegistrationSource());

        //    // OPTIONAL: Enable property injection into action filters.
        //    builder.RegisterFilterProvider();

        //    // OPTIONAL: Enable action method parameter injection (RARE).
        //    //   builder.InjectActionInvoker();

        //    // Set the dependency resolver to be Autofac.
        //    this.RegistInAutofac(builder);
        //    var container = builder.Build();
        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}

        //private void RegistInAutofac(ContainerBuilder builder)
        //{
        //    //builder.RegisterType<ProductRepository>().As<IProductRepository>();

        //    //builder.RegisterType<ManufacturerService>().As<IManufacturerService>();

        //    //builder.RegisterType<ProductService>().As<IProductService>();
        //    //builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();
        //    builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly).Where(t => t.Name.EndsWith("Repository"));
        //    builder.RegisterAssemblyTypes(typeof(ProductService).Assembly).Where(t => t.Name.EndsWith("Service"));
        //}
    }
}
