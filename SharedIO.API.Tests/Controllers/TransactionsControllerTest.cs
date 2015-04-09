using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedIO.Controllers;
using SharedIO.Model;

namespace SharedIO.API.Tests.Controllers
{
    [TestClass]
    public class TransactionsControllerTest : BaseRavenTest
    {

        [TestInitialize]
        public void Init()
        {
            SetUp();
        }
//
//        [TestMethod]
//        public void GetUsersTransactions_Test()
//        {
//            TransactionsController controller = new TransactionsController();
//            //FIXME what about balance checking ?
//            RavenSession.Store(new Identity { Email = "tester@tester.com", Id="tester1", Name = "Tester", UserName="username"});
//            RavenSession.Store(new Transaction { PayeeId = "tester1" , PayerId = "different", });
//            RavenSession.Store(new Transaction { PayeeId = "different", PayerId = "tester1" });
//            RavenSession.Store(new Transaction { PayeeId = "bothdifferent" , PayerId = "bothdifferent" });
//
//            RavenSession.SaveChanges();
//
//            controller.RavenSession = RavenSession;
//            var result = controller.GetAll();
//            Assert.AreEqual(result.Count(), 2);
//
//            CleanUp();
//        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }
    }
}
