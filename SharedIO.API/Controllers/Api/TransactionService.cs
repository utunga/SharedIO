using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using SharedIO.Model;

namespace SharedIO.API.Controllers.Api
{

    public interface ITransactionService
    {
        Transaction GetTransaction(string id);
        IEnumerable<Transaction> GetTransactionsFor(string accountID);
        decimal GetBalance(string accountID);
        void Store(Transaction transaction);
        void Delete(string id);
    }

    public class TransactionService : ITransactionService
    {
        private IDocumentSession _ravenSession;
        public TransactionService(IDocumentSession ravenSession)
        {
            _ravenSession = ravenSession;
        }
        public Transaction GetTransaction(string id)
        {
            return _ravenSession.Load<Transaction>(id);
        }

        public IEnumerable<Transaction> GetTransactionsFor(string accountID)
        {
            return
                _ravenSession.Query<Transaction>()
                    .Where(x => x.PayerId == accountID || x.PayeeId == accountID)
                    .OrderBy(x => x.Created);
        }

        public decimal GetBalance(string accountID)
        {
            decimal bal = 0M;
            foreach (var transaction in GetTransactionsFor(accountID))
            {
                if (transaction.PayeeId == accountID)
                {
                    bal = bal + transaction.Amount;
                }
                else if (transaction.PayerId == accountID)
                {
                    bal = bal - transaction.Amount;
                }
            }
            return bal;
        }

        public void Store(Transaction transaction)
        {
            if (transaction.Created==DateTime.MinValue)
                transaction.Created = DateTime.UtcNow;
            _ravenSession.Store(transaction);
        }

        public void Delete(string id)
        {
            var transaction = GetTransaction(id);
            _ravenSession.Delete<Transaction>(transaction);
        }

    }
}