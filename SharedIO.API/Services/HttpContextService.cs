using System.Web;

namespace SharedIO.API.Services
{
    public interface IHttpContextService
    {
        string AccountName { get; }
        bool AccountIsAuthenticated { get; }
    }

    public class HttpContextService : IHttpContextService
    {
        public string AccountName
        {
            get { return CurrentHttpContext.User.Identity.Name; }
        }

        public bool AccountIsAuthenticated
        {
            get { return CurrentHttpContext.User.Identity.IsAuthenticated; }
        }

        static HttpContext CurrentHttpContext
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                {
                    throw new System.ApplicationException("HttpContext.Current is null");
                }
                return context;
            }
        }
    }
}