using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotNetBay.WebApi
{
    [RoutePrefix("api")]
    public class StatusController : ApiController
    {
        [Route("status")]
        [HttpGet]
        public IHttpActionResult AreYouFine()
        {
            return Ok("I'm fine");
        }
    }
}
