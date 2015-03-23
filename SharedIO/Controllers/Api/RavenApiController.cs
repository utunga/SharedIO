using System;
using System.Web.Http;
using System.Web.Mvc;
using AssetManager;
using Raven.Client;
using Raven.Client.Document;

namespace SharedIO.Controllers
{
    [RequireHttps]
    public abstract class RavenAPIController : ApiController
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

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            if (RavenSession == null)
                RavenSession = WebApiApplication.Store.OpenSession();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            using (RavenSession)
            {
                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }


        // For normal controllers use this..
//        protected override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            RavenSession = RavenStore.OpenSession();
//            base.OnActionExecuting(filterContext);
//        }
//
//        protected override void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            using (RavenSession)
//            {
//                if (RavenSession != null && filterContext.Exception == null)
//                {
//                    RavenSession.SaveChanges();
//                }
//            }
//            base.OnActionExecuted(filterContext);
//        }
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