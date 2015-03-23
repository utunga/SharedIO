using System.Collections.Generic;
using System.Linq;
using SharedIO.Model;

namespace SharedIO.Controllers
{
    public class BaseController : RavenAPIController
    {    
        protected Identity ObtainCurrentIdentity()
        {
            //return RavenSession.Query<Identity>().SingleOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);
            return RavenSession.Query<Identity>().SingleOrDefault(x => x.UserName == "test");
        }
        
        public Transaction GetTransaction(string id)
        {
            return RavenSession.Query<Transaction>().SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions(string userID)
        {
            return RavenSession.Query<Transaction>().Where(x => x.PayerId == userID || x.PayeeId == userID);
        }

    }
}