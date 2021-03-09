using Queue.Action;
using Queue.Models;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    [RoutePrefix("api/UserHardware")]
    public class UserHardwareController : ApiController
    {
        aUtilities ut = new aUtilities();

        [HttpPost]       
        [Route("Hardware")]
        public HttpResponseMessage Savehardware(HardwareModel t)
        {
            aAutomaticTakeTime s = new aAutomaticTakeTime();
            return ut.ReturnResponse(s.SaveHardware(t));
        }
    }
}