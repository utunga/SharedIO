using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;
using Raven.Client;
using SharedIO.Model;

namespace SharedIO.API.Services
{
    public interface IAccountService
    {
        Account CurrentAccount { get; }
        Account GetAccount(string accountId);
        Account GetAccountByAccountName(string accountName);
        void SaveAccount(Account account);
        void DeleteAccount(string accountId);
    }

//    public class AccountService : IAccountService
//    {
//        readonly IDocumentSession session;
//        readonly IHttpContextService context;
//
//        public AccountService(IDocumentSession session, IHttpContextService context)
//        {
//            this.session = session;
//            this.context = context;
//        }
//
//        public Account CurrentAccount
//        {
//            get
//            {
//                if (!context.AccountIsAuthenticated) return null;
//                var accountId = string.Format("accounts/{0}", context.AccountName);
//                return session.Load<Account>(accountId);
//            }
//        }
//
//        public Account GetAccount(string accountId)
//        {
//            if (accountId == null)
//            {
//                throw new ArgumentNullException("accountId");
//            }
//
//            return session.Load<Account>(accountId);
//        }
//
//        public Account GetAccountByAccountName(string accountName)
//        {
//            if (accountName == null)
//            {
//                throw new ArgumentNullException("accountName");
//            }
//
//            return session.Load<Account>(Account.AccountIdFromAccountName(accountName));
//        }
//
//        public void SaveAccount(Account account)
//        {
//            if (account == null)
//            {
//                throw new ArgumentNullException("account");
//            }
//
//            session.Store(account);
//        }
//
//
//        public void DeleteAccount(string accountId)
//        {
//            //session.Advanced.DatabaseCommands.Delete(accountId, null);
//        }
//    }
}