using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopSaleForPad.Controllers
{
    public class SaleApiController : ApiController
    {
        // GET api/saleapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/saleapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/saleapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/saleapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/saleapi/5
        public void Delete(int id)
        {
        }
    }
}
