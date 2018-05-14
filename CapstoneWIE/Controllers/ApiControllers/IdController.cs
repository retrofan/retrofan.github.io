using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    public class IdController : ApiController
    {
        [HttpGet]
        public string Get()
        {
            return RequestContext.Principal.Identity.GetUserId();
        }
    }
}
