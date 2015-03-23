using System.Collections.Generic;
using System.Linq;
using SharedIO.Model;

namespace SharedIO.API.Controllers.Api
{
    public abstract class BaseController : RavenAPIController
    {    
        protected Identity ObtainCurrentIdentity()
        {
            return RavenSession.Query<Identity>().SingleOrDefault(x => x.UserName == User.Identity.Name);
            //return RavenSession.Query<Identity>().SingleOrDefault(x => x.UserName == "test");
        }
       

    }
}