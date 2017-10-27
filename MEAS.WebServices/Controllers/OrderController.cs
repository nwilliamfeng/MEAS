using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MEAS.WebServices.Controllers
{
    public class OrderController : ApiController
    {
        // GET: api/Api
        [ActionName("loadall")]
        [HttpGet]
        public IEnumerable<string> LoadAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Api/5
        [ActionName("load")]
        [HttpGet]
        public string Load(int id)
        {
            return "value"+id.ToString();
        }

        // POST: api/Api
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Api/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Api/5
        public void Delete(int id)
        {
        }
    }
}
