using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raven.Client.Linq;
using SharedIO.Model;

namespace SharedIO.API.Controllers.Api
{
    public class MemberController : BaseController
    {
        //SPECNOTE http://jsonapi.org/format/

        private IMemberProfile GetMemberProfile(string id)
        {
            return RavenSession.Query<IMemberProfile>().FirstOrDefault(x => x.id == id);
        }

        // GET: api/members
        [Route("api/members")]
        [HttpGet]
        public IEnumerable<IMemberProfile> GetAll()
        {
            return RavenSession.Query<Member>();
        }

        // GET: api/members/00amyWGct0y_ze4lIsj2Mw
        [Route("api/members/{id}")]
        [HttpGet]
        public IMemberProfile Get(string id)
        {
            //id is a ShortGuid
            return GetMemberProfile(id);
        }

        // POST: api/members
        [Route("api/members")]
        [HttpPost]
        public IMemberProfile Post(Member newMember)
        {
            newMember.created = DateTime.UtcNow;
            newMember.id = ShortGuid.Encode(Guid.NewGuid());
            RavenSession.Store(newMember);
            return Get(newMember.Id);
        }

        // PUT: api/members/00amyWGct0y_ze4lIsj2Mw
        public IMemberProfile Put(string id, Member updated)
        {
            var existing = GetMemberProfile(id);
            existing.aboutme = updated.aboutme;
            existing.geo = updated.geo;
            existing.locality = updated.locality;
            existing.name = updated.name;
            existing.portrait = updated.portrait;
            RavenSession.Store(existing);
            return Get(id);
        }

        // DELETE: api/members/00amyWGct0y_ze4lIsj2Mw
        public void Delete(string id)
        {
            var existing = GetMemberProfile(id);
            RavenSession.Delete<IMemberProfile>(existing);
        }
    }
}
