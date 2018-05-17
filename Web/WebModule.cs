using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Data;
using Web.ModelBinders;
using Web.Models;
using Web.Models.Account;
using Web.Service;
using Module = Autofac.Module;
using Data.Repository;
using Domain.Interfaces;

namespace Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            // Register ASP.NET MVC stuff.
            builder.RegisterControllers(Assembly.GetExecutingAssembly())
                .InjectActionInvoker()
                .PropertiesAutowired();

            builder.RegisterFilterProvider();
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterType<ModelBindingHelper>();
            // Configure Autofac to inject action method parameters.
            builder.RegisterType<ExtensibleActionInvoker>()
                .As<IActionInvoker>()
                .WithParameter("injectActionMethodParameters", true);

            builder.RegisterModule<DataModule>();
            builder.RegisterType<EmailUserCreationConfirmationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            builder.RegisterType<EmailResetPasswordInformationService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<RemittanceReportGenerator>()
                .As<IRemittanceReportGenerator>()
                .InstancePerLifetimeScope(); 


            builder.Register(c => new EmailSender(25, "localhost", "surenth.j@sagitec.com")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<UsersContext>()
                .AsSelf()
               .InstancePerLifetimeScope();

        }
    }
}