using System.Collections.Generic;
using System.Linq;
using AspNet.Identity.RavenDB.Entities;
using Microsoft.AspNet.Identity;
using Raven.Client;
using SharedIO.Model;

namespace SharedIO.API.Controllers.Api
{
    public abstract class BaseController : RavenAPIController
    {
        protected BaseController()
        {}
        protected BaseController(IDocumentSession ravenSession) : base(ravenSession)
        {
        }

        protected User ObtainCurrentUser()
        {
            // this kinda doesnt make sense 
            return RavenSession.Query<User>().SingleOrDefault(x => x.UserName == User.Identity.GetUserName());
            //return RavenSession.Query<Identity>().SingleOrDefault(x => x.UserName == "test");
        }


    }
}