using System.Web.Mvc;

namespace SharedIO.API.Controllers
{
    public class RedirectController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/Help");
        }
    }
}