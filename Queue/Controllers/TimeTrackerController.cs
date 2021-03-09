using Queue.Action;
using Queue.Models;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    [RoutePrefix("api/TimeTracker")]
    public class TimeTrackerController : ApiController
    {
        aUtilities ut = new aUtilities();

        [HttpPost]
        [Route("CreateTracker")]
        public HttpResponseMessage CreateTracker(TrackerModel t)
        {
            aAutomaticTakeTime s = new aAutomaticTakeTime();
            return ut.ReturnResponse(s.CreateAutomaticTakeTime(t));
        }
    }
}