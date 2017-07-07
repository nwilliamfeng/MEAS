using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac;
using MEAS.Service;
using MEAS.Data.MySql;

namespace MEAS
{
    public class DependencyResolverConfig
    {
        public static void RegistInstances()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
           
            builder.RegisterAssemblyTypes(typeof(ProductRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();

            //   builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
            //.Where(t => t.Name.EndsWith("Authentication"))
            //.AsImplementedInterfaces().InstancePerHttpRequest();

            //builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SocialGoalEntities())))
            //    .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}