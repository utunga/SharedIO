using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using AssetManager.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raven.Client;
using Raven.Client.Embedded;
using SharedIO.Controllers;
using SharedIO.Model;

namespace AssetManager.Tests.Controllers
{
    [TestClass]
    public class TransactionsControllerTest : ParentControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            SetUp();
        }

        [TestMethod]
        public void GetUsersTransactions_Test()
        {
            TransactionsController controller = new TransactionsController();
            //FIXME what about balance checking
            RavenSession.Store(new Identity { Email = "tester@tester.com", Id="tester1", Name = "Tester", UserName="username"});
            RavenSession.Store(new Transaction { PayeeId = "tester1" , PayerId = "different", });
            RavenSession.Store(new Transaction { PayeeId = "different", PayerId = "tester1" });
            RavenSession.Store(new Transaction { PayeeId = "bothdifferent" , PayerId = "bothdifferent" });

            RavenSession.SaveChanges();

            controller.RavenSession = RavenSession;
            var result = controller.GetAll();
            Assert.AreEqual(result.Count(), 2);

            CleanUp();
        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }
    }
}
