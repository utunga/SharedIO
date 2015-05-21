using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedIO.API.Controllers.Api;
using SharedIO.Controllers;
using SharedIO.Model;

namespace SharedIO.API.Tests.Controllers
{
    [TestClass]
    public class MemberControllerTests : BaseRavenTest
    {

        [TestInitialize]
        public void Init()
        {
            SetUp();
        }

       
        [TestMethod]
        public void GetAll_Test()
        {
            RavenSession.Store(new Account()
            {
                Name =  "Joe",
                About = "Friendly"
            });
            RavenSession.SaveChanges();

            MemberController controller = new MemberController(RavenSession);
            var result = controller.GetAll();
            Assert.AreEqual(1, result.Count());

            CleanUp();
        }

        [TestMethod]
        public void GetOne_Test()
        {
            MemberController controller = new MemberController(RavenSession);
            controller.RavenSession = RavenSession;
            var account = new Account()
            {
                Name = "Joe",
                About = "Friendly"
            };
            account.Tags.Add("friendly");
            account.Tags.Add("male");
            RavenSession.Store(account);
            RavenSession.SaveChanges();

            var result = controller.Get(account.Id);

            Assert.AreEqual(account.Name, result.name);
            Assert.AreEqual(account.About, result.aboutme);
            AssertListEquality(new [] { "friendly", "male"}, result.tags);
        }



        [TestMethod]
        public void Register_Test()
        {
             
        }


        [TestCleanup]
        public void Clean()
        {
           //CleanUp();
        }
    }
}
