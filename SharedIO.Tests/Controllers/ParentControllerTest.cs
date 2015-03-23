﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;

namespace AssetManager.Tests.Controllers
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

            SetUpIdentity();

            RavenSession = WebApiApplication.Store.OpenSession();
        }

        protected void CleanUp()
        {
            WebApiApplication.Store.Dispose();
        }
    }
}
