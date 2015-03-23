using System;
using Microsoft.AspNet.Mvc;
using Raven.Client;
using Raven.Client.Document;
using System.Threading.Tasks;
using System.Net.Http;

namespace SharedIO.Controllers
{
    [RequireHttps]
    public abstract class RavenAPIController : Controller
    {
        public IDocumentStore RavenStore
        {
            get { return LazyDocStore.Value; }
        }

        private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "WebApiSample"
            };

            docStore.Initialize();
            return docStore;
        });

        public IDocumentSession RavenSession { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = RavenStore.OpenSession();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (RavenSession)
            {
                if (RavenSession != null && filterContext.Exception == null)
                {
                     RavenSession.SaveChanges();
                }
            }
            base.OnActionExecuted(filterContext);
        }

        //public async override Task<HttpResponseMessage> ExecuteAsync(
        //    HttpControllerContext controllerContext,
        //    CancellationToken cancellationToken)
        //{
        //    using (Session = Store.OpenAsyncSession())
        //    {
        //        var result = await base.ExecuteAsync(controllerContext, cancellationToken);
        //        await Session.SaveChangesAsync();

        //        return result;
        //    }
        //}
    }
}