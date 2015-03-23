using System.IO;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace SharedIO.API.Tests.Controllers
{
    public class ParentControllerTest
    {
        protected IDocumentSession RavenSession
        {
            get;
            set;
        }

        protected void SetUpIdentity()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            // User is logged in
            GenericPrincipal principal = new GenericPrincipal(new GenericIdentity("username"), null);
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }

        protected void SetUp()
        {
            WebApiApplication.Store = new EmbeddableDocumentStore { RunInMemory = true };
            WebApiApplication.Store.Initialize();

//            //FIXME1 it's no good to have unit tests depend on a service
//            // but for now, we'll leave it like this 
//            var docStore = new DocumentStore
//            {
//                Url = "http://localhost:8080",
//                DefaultDatabase = "cpassets"
//            };
//
//            docStore.Initialize();
//            WebApiApplication.Store = docStore;

            SetUpIdentity();

            RavenSession = WebApiApplication.Store.OpenSession();
        }

        protected void CleanUp()
        {
            WebApiApplication.Store.Dispose();
        }
    }
}
