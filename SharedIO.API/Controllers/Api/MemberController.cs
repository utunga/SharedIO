using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Raven.Client;
using Raven.Client.Linq;
using SharedIO.Model;
using SharedIO.API.Services;

namespace SharedIO.API.Controllers.Api
{
    public class MemberProfileViewModel
    {
        // SPECCHANGE: id is implied to be an int in the spec but I think string is more general, 
        //             and would leaves it as implementation detail, which can be v important with things like ids
        public string id { get; set; }
        public string name { get; set; }
        public double latitude { get; set; } // SPECNOTE: called 'geo' in spec but 'coordinates' would match GeoJSON.org
        public double longitude { get; set; }
        public DateTime created { get; set; } // IMPLNOTE: serialized to a javascript datetime like '1297373635' as serialization detail
        public string portrait { get; set; } //  SPECNOTE: should this be portrait_url or profile_image_url a la twitter profiles?
        public string locality { get; set; } //  SPECNOTE: or we could make the location above a geoJSon object with optional coordinates
        public decimal balance { get; set; } // SPECNOTE: This is ViewModel property only, dependent on transactions store

        public string aboutme { get; set; } //SPECNOTE: I think I would prefer just 'about'

        public ICollection<Tag> tags { get; set; }

        public static MemberProfileViewModel FromAccount(Account account, decimal balance)
        {
            return new MemberProfileViewModel()
            {
                id = account.Id,
                name = account.Name,
                latitude = account.Latitude,
                longitude = account.Longitude,
                locality = account.Locality,
                portrait = account.Portrait,
                aboutme = account.About,
                created = account.Created,
                balance = balance
            };
        }
    }


    public class MemberController : BaseController
    {
        //SPECNOTE http://jsonapi.org/format/
        ITransactionService _transactionService;


        public MemberController(IDocumentSession ravenSession) : base(ravenSession)
        {
            _transactionService = new TransactionService(ravenSession);
        }

        public MemberProfileViewModel GetMemberProfile(string id)
        {
            var account = RavenSession.Query<Account>().FirstOrDefault(x => x.Id == id);
            var balance = _transactionService.GetBalance(account.Id);
            return MemberProfileViewModel.FromAccount(account, balance);
        }


        // GET: api/members
        [Route("api/members")]
        [HttpGet]
        public IEnumerable<MemberProfileViewModel> GetAll()
        {
            var results = new List<Account>(RavenSession.Query<Account>().OrderBy(x => x.Id));
            return results.Any() 
                ? results.Select(x => MemberProfileViewModel.FromAccount(x, _transactionService.GetBalance(x.Id)))
                : new MemberProfileViewModel[0];
        }

        // GET: api/members/2
        [Route("api/members/{id}")]
        [HttpGet]
        public MemberProfileViewModel Get(string id)
        {
            var account = RavenSession.Load<Account>(id);
            return MemberProfileViewModel.FromAccount(account, _transactionService.GetBalance(account.Id));
        }

//        // POST: api/members
//        [Route("api/members")]
//        [HttpPost]
//        public MemberProfileViewModel Post(Account newMember)
//        {
//            newMember.created = DateTime.UtcNow;
//            newMember.id = ShortGuid.Encode(Guid.NewGuid());
//            RavenSession.Store(newMember);
//            return Get(newMember.Id);
//        }
//
//        // PUT: api/members/00amyWGct0y_ze4lIsj2Mw
//        public MemberProfileViewModel Put(string id, Account updated)
//        {
//            var existing = GetMemberProfile(id);
//            existing.aboutme = updated.aboutme;
//            existing.geo = updated.geo;
//            existing.locality = updated.locality;
//            existing.name = updated.name;
//            existing.portrait = updated.portrait;
//            RavenSession.Store(existing);
//            return Get(id);
//        }
//
//        // DELETE: api/members/00amyWGct0y_ze4lIsj2Mw
//        public void Delete(string id)
//        {
//            var existing = GetMemberProfile(id);
//            RavenSession.Delete<MemberProfileViewModel>(existing);
//        }
    }
}
