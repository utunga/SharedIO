using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Http;
using System.Web.Mvc;
using SharedIO.Model;

namespace SharedIO.Controllers
{
   
    public class TransactionsController : BaseController
    {

        // GET api/transactions
        //[Authorize]
        public IEnumerable<object> GetAll()
        {
            var currentIdentity = ObtainCurrentIdentity();
            return GetTransactions("" + currentIdentity.Id);
        }

        // GET api/transactions/5
        public Transaction Get(string id)
        {
            return GetTransaction(id);
        }

        // POST api/transactions
        public object Post(Transaction value)
        {
            value.Created = DateTime.UtcNow;
            RavenSession.Store(value);
            return GetReturnSaveReturnDto(value);
        }

        // PUT api/transactions/ab32ce3232d
        public object Put(string id, Transaction value)
        {
            var owner = ObtainCurrentIdentity();
            var transaction = GetTransaction(id);
            transaction.Amount = value.Amount;
            transaction.PayerId = value.PayerId;
            transaction.PayeeId = value.PayeeId;
            transaction.Description = value.Description;
            transaction.Categories = value.Categories;

            return GetReturnSaveReturnDto(transaction);
        }

        // DELETE api/transactions/ab32ce3232d
        public void Delete(string id)
        {
           var transaction = GetTransaction(id);
           RavenSession.Delete<Transaction>(transaction);
        }
        private object GetReturnSaveReturnDto(Transaction transaction)
        {
            var returnValue = new
            {
                transaction = transaction,
                message = "Saved OK"
            };
            return returnValue;
        }
    }
}
