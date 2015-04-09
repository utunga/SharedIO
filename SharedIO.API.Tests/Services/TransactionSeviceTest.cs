using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedIO.API.Controllers.Api;
using SharedIO.Model;

namespace SharedIO.API.Tests.Services
{
    [TestClass]
    public class TransactionSeviceTest : BaseRavenTest
    {
        [TestInitialize]
        public void Init()
        {
            SetUp();
        }

        [TestCleanup]
        public void Clean()
        {
            CleanUp();
        }

        [TestMethod]
        public void Store_PersistsData()
        {
            var target = new TransactionService(RavenSession);
            var test = new Transaction()
            {
                PayeeId = "account/12",
                PayerId = "account/10",
                Amount = 12.2M
            };

            target.Store(test);
            RavenSession.SaveChanges();
            Assert.IsNotNull(test.Id);

            var result = target.GetTransaction(test.Id);
            Assert.AreSame(result, test);
        }

        [TestMethod]
        public void GetTransactions_GivesAllTransactions()
        {
            var testAccountId = "account/10";
            var target = new TransactionService(RavenSession);

            target.Store(new Transaction()
            {
                PayeeId = testAccountId,
                PayerId = "account/12",
                Amount = 12M
            });
            target.Store(new Transaction()
            {
                PayeeId = "account/12",
                PayerId = testAccountId,
                Amount = 6M
            });
            target.Store(new Transaction()
            {
                PayeeId = testAccountId,
                PayerId = "account/14",
                Amount = 5M
            });

            RavenSession.SaveChanges();

            var result = target.GetTransactionsFor(testAccountId);
            var list = new List<Transaction>(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(12M, list[0].Amount);
            Assert.AreEqual(6M, list[1].Amount);
            Assert.AreEqual(5M, list[2].Amount);
        }

        [TestMethod]
        public void GetBalance_AggregatesCorrectly()
        {
            var testAccountId = "account/10";
            var target = new TransactionService(RavenSession);
           
            target.Store(new Transaction()
            {
                PayeeId = testAccountId,
                PayerId = "account/12",
                Amount = 12.2M
            });
            target.Store(new Transaction()
            {
                PayeeId = "account/12",
                PayerId = testAccountId,
                Amount = .2M
            });
            target.Store(new Transaction()
            {
                PayeeId = testAccountId,
                PayerId = "account/14",
                Amount = 5M
            });

            RavenSession.SaveChanges();

            var result = target.GetBalance(testAccountId);
            Assert.AreEqual(17, result);
        }
    }
}
