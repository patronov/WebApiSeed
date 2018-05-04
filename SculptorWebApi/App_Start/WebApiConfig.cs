using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using Autofac;
using System.Reflection;
using System.Data.Entity;
using SculptorWebApi.DataAccess.Classes;
using SculptorWebApi.DataAccess.Interfaces;
using SculptorWebApi.Controllers;
using Autofac.Integration.WebApi;
using SculptorWebApi.DataAccess;
using SculptorWebApi.Services.DataAccessServices.Classes;
using SculptorWebApi.Services.DataAccessServices.Classes.Interfaces;
using Unity;
using Unity.Lifetime;
using SculptorWebApi.Main.Logging;
using SculptorWebApi.Main.Tracing;
using Autofac.Extras.NLog;

namespace SculptorWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            LoggingSetup(config);
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var cors = new EnableCorsAttribute(
                origins: "*", headers: "*", methods: "*",
                exposedHeaders: "X-Paging-PageCount,X-Paging-PageNo,X-Paging-PageSize,X-Paging-TotalRecordCount")
            {
                SupportsCredentials = true
            };
            config.EnableCors(cors);

            AutofacSetup(config);
            //UnitySetup(config);
        }

        private static void AutofacSetup(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule<SimpleNLogModule>();
            builder.RegisterType<SculptorContext>().As<DbContext>().InstancePerRequest();
            builder.RegisterType<PredictiveModelService>().As<IPredictiveModelService>().InstancePerRequest();

            builder.RegisterType<PredictiveModelsController>().InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void UnitySetup(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, SculptorContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IPredictiveModelService, PredictiveModelService>(new HierarchicalLifetimeManager());

            container.RegisterType<PredictiveModelsController>();// (new HierarchicalLifetimeManager());

            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            //config.DependencyResolver = new UnityResolver(container);
        }

        private static void TracingSetup(HttpConfiguration config)
        {
            config.Services.Replace(typeof(ITraceWriter), new SimpleTracer());
        }

        private static void LoggingSetup(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new CustomLogHandler());
        }
    }
}
