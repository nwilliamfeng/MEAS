using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Core.Lifetime;
using MEAS.Service;
using MEAS.Data;
using MEAS.Data.SqlServer;
using MEAS.Binder;

namespace MEAS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            this.InitizeAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartBinder()); //Page 191
        }

        private void InitizeAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            //   builder.InjectActionInvoker();

            // Set the dependency resolver to be Autofac.
            this.RegistInAutofac(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void RegistInAutofac(ContainerBuilder builder)
        {
     
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterControllers(typeof(MEAS.Controllers.ManufacturerController).Assembly);
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();
        }
    }
}
