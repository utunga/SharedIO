using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SharedIO.API.Controllers.Api
{
    public class CategoriesController : ApiController
    {
        // GET: api/Categories
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Categories/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Categories
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Categories/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {
        }
    }
}
