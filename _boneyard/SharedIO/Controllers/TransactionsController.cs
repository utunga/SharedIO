using System;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using SharedIO.Model;

namespace SharedIO.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : BaseController
    {

        // GET api/transactions
        //[Authorize]
        [HttpGet]
        public IEnumerable<object> GetAll()
        {
            var owner = ObtainCurrentIdentity();
            return GetTransactions(owner.Id);
        }

        // GET api/transactions/5
        [HttpGet("{id:int}", Name = "GetByIdRoute")]
        public Transaction Get(string id)
        {
            return GetTransaction(id);
        }

        // POST api/transactions
        [HttpPost]
        public object Post([FromBody]Transaction value)
        {
            value.Created = DateTime.UtcNow;
            RavenSession.Store(value);
            return GetReturnSaveReturnDto(value);
        }

        // PUT api/transactions/ab32ce3232d
        [HttpPut]
        public object Put(string id, [FromBody]Transaction value)
        {
            var owner = ObtainCurrentIdentity();
            var transaction = GetTransaction(id);
            transaction.Quantity = value.Quantity;
            transaction.PayerId = value.PayerId;
            transaction.PayeeId = value.PayeeId;
            transaction.Description = value.Description;
            transaction.Categories = value.Categories;

            return GetReturnSaveReturnDto(transaction);
        }

        // DELETE api/transactions/ab32ce3232d
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
           var transaction = GetTransaction(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            RavenSession.Delete<Transaction>(transaction);
            return new HttpStatusCodeResult(204); // 201 No Content
        }
        private Object GetReturnSaveReturnDto(Transaction transaction)
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
