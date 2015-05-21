using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Embedded;

namespace SharedIO.API.Tests
{
    public class BaseRavenTest
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

            SetUpIdentity();

            RavenSession = WebApiApplication.Store.OpenSession();
        }

        protected void CleanUp()
        {
            WebApiApplication.Store.Dispose();
        }

        protected static void AssertListEquality<T>(IEnumerable<T> expectedEnumerable, IEnumerable<T> actualEnumerable, string message = "")
        {
            var expecteds = new List<T>(expectedEnumerable);
            var actuals = new List<T>(actualEnumerable);
            bool theyMatch = actuals.All(actual => expecteds.Contains(actual)) && expecteds.All(expected => actuals.Contains(expected));
            if (!theyMatch)
            {
                string expectedsStr = expecteds.Count() == 0 ? "{empty list}" : string.Join(",", expecteds.Select(x => "" + x).ToArray());
                string actualsStr = actuals.Count() == 0 ? "{empty list}" : string.Join(",", actuals.Select(x => "" + x).ToArray());
                Assert.Fail(string.Format("{0} - Expected:'{1}' Actual:'{2}'", message, expectedsStr, actualsStr));
            }
        }
    }
}
