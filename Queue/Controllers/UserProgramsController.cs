using Queue.Action;
using Queue.Models;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    [RoutePrefix("api/UserPrograms")]
    public class UserProgramsController : ApiController
    {
        aUtilities ut = new aUtilities();

        [HttpPost]
        [Route("Installed")]
        public HttpResponseMessage SaveIntalledPrograms(InstalledModel t)
        {
            aAutomaticTakeTime s = new aAutomaticTakeTime();
            return ut.ReturnResponse(s.SaveIntalledPrograms(t));
        }
    }
}