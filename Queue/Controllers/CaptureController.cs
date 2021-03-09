using Queue.Action;
using Queue.Models;
using System.Net.Http;
using System.Web.Http;

namespace Queue.Controllers
{
    [RoutePrefix("api/Capture")]
    public class CaptureController : ApiController
    {
        aUtilities ut = new aUtilities();

        [HttpPost]
        [Route("WindowsCapture")]
        public HttpResponseMessage WindowsCapture(CaptureModel t)
        {
            aAutomaticTakeTime s = new aAutomaticTakeTime();
            return ut.ReturnResponse(s.CreateCapture(t));
        }
    }
}