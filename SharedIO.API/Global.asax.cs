using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Client.Indexes;
using SharedIO.Model;

namespace SharedIO.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RavenDBInit();
            CreateTestData();


        }

        private void CreateTestData()
        {
            using (var ravenSession = WebApiApplication.Store.OpenSession())
            {
                /*
                var current = ravenSession.Query<Account>();
                if (!current.Any())
                {
                    ravenSession.Store(new Account()
                    {
                        Name = "Joe",
                        About = "Friendly"
                    });
                    ravenSession.SaveChanges();
                }
                */
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // redirect *all* 404 errors to the /Help documentation
            // FIXME - this is a bit of a hack but will do for now
            Exception ex = Server.GetLastError();
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            {
                Response.Redirect("Help");
            }
        }

        private static void RavenDBInit()
        {
            var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
            parser.Parse();

            Store = new DocumentStore
            {
                ApiKey = parser.ConnectionStringOptions.ApiKey,
                Url = parser.ConnectionStringOptions.Url,
            };

            Store.Initialize();

            IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), Store);

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
        }

        public static DocumentStore Store;
    }
}
