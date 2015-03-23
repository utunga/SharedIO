using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedIO.API.Controllers.Api;
using SharedIO.Controllers;
using SharedIO.Model;

namespace SharedIO.API.Tests.Controllers
{
    [TestClass]
    public class MemberControllerTests : ParentControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            SetUp();
        }

        [TestMethod]
        public void GetAll_Test()
        {
           
            //FIXME what about balance checking ?
            RavenSession.Store(new Member()
            {
                name =  "Joe",
                aboutme = "Friendly"
            });
         
            RavenSession.SaveChanges();

            MemberController controller = new MemberController();
            controller.RavenSession = RavenSession;
            var result = controller.GetAll();
            Assert.AreEqual(1, result.Count());

            CleanUp();
        }


        [TestMethod]
        public void GetOne_Test()
        {

            MemberController controller = new MemberController();
            controller.RavenSession = RavenSession;
            var member = new Member()
            {
                name = "Joe",
                aboutme = "Friendly"
            };
            var retVal = controller.Post(member);
            var target = controller.Get(retVal.id);

            Assert.AreEqual(retVal, target);

            CleanUp();
        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }
    }
}
